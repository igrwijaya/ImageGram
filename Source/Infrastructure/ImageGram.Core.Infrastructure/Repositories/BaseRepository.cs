using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ImageGram.Core.Domain.Commons;
using ImageGram.Core.Infrastructure.DataSources;
using Microsoft.EntityFrameworkCore;

namespace ImageGram.Core.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        #region Constructors

        public BaseRepository(CoreDbContext context)
        {
            Context = context;
        }

        #endregion

        #region Protected Properties

        protected CoreDbContext Context { get; }

        #endregion

        #region IBaseRepository Members

        public async Task<int> CreateAsync(TEntity entity)
        {
            var dbSet = Context.Set<TEntity>();

            await dbSet.AddAsync(entity);
            await Context.SaveChangesAsync();

            var entityIdProperty = entity.GetType().GetProperty("Id");

            return entityIdProperty is null
                ? 0
                : int.Parse(entityIdProperty.GetValue(entity)?.ToString() ?? "0");
        }

        public async Task<TEntity> ReadAsync(int id)
        {
            var dbSet = Context.Set<TEntity>();
            
            return await dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dbSet = Context.Set<TEntity>();
            var entity = await dbSet.FindAsync(id);

            if (entity == null)
            {
                return;
            }

            dbSet.Remove(entity);

            await Context.SaveChangesAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            var entity = await ReadAsync(id);

            return entity != null;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var dbSet = Context.Set<TEntity>();

            return dbSet.AsNoTracking().ToList();
        }

        #endregion
        
        #region Protected Methods

        protected Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var dbSet = Context.Set<TEntity>();

            return dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        protected Task<TEntity> ReadAsync(Func<DbSet<TEntity>, IQueryable<TEntity>> extendedQuery, Expression<Func<TEntity, bool>> predicate)
        {
            var dbSet = Context.Set<TEntity>();
            var entities = extendedQuery != null ? extendedQuery.Invoke(dbSet) : dbSet;

            return entities.Where(predicate).FirstOrDefaultAsync();
        }

        protected async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var dbSet = Context.Set<TEntity>();
            var entities = dbSet.Where(predicate);
            
            dbSet.RemoveRange(entities);
            
            await Context.SaveChangesAsync();
        }

        protected CoreDataTable<TDataTable> GetDataTable<TDataTable>(
            CoreDataTableParameter parameter,
            Func<TEntity, TDataTable> modelMapping)
        {
            var dbSet = Context.Set<TEntity>();

            //Paging Size
            var pageSize = parameter.Length;
            var skip = parameter.Start;

            // Getting all data  
            var entities = dbSet.AsQueryable();

            var isPropertyAvailable = typeof(TEntity).GetProperties().Any(item => item.Name == parameter.SortColumn);

            //Sorting  
            if (!(string.IsNullOrEmpty(parameter.SortColumn) && string.IsNullOrEmpty(parameter.SortColumnDirection)) && isPropertyAvailable)
            {
                entities = entities.OrderBy(parameter.SortColumn + " " + parameter.SortColumnDirection);
            }

            //Search  
            if (!string.IsNullOrEmpty(parameter.SearchParam))
            {
                // var whereConditions = typeof(TEntity).GetProperties()
                //     .Select(propertyInfo => $"{propertyInfo.Name} LIKE '%{parameter.SearchParam}%'")
                //     .ToList();
                //
                // entities = entities.Where(string.Join(" OR ", whereConditions));
                // customerData = customerData.Where("");
            }

            //total number of rows count   
            var recordsTotal = entities.Count();

            //Paging
            var data = entities.Skip(skip).Take(pageSize).ToList();
            var modelResult = data.Select(modelMapping).ToList();

            return new CoreDataTable<TDataTable>
            {
                Data = modelResult,
                Draw = parameter.Draw,
                RecordsFiltered = recordsTotal,
                RecordsTotal = recordsTotal
            };
        }
        
        protected CoreDataTable<TDataTable> GetDataTable<TDataTable>(
            CoreDataTableParameter parameter,
            Func<DbSet<TEntity>, IQueryable<TEntity>> extendedQuery,
            Func<TEntity, TDataTable> modelMapping)
        {
            var dbSet = Context.Set<TEntity>();

            //Paging Size
            var pageSize = parameter.Length;
            var skip = parameter.Start;

            // Getting all data  
            var entities = extendedQuery != null ? extendedQuery.Invoke(dbSet) : dbSet;

            var isPropertyAvailable = typeof(TEntity).GetProperties().Any(item => item.Name == parameter.SortColumn);

            //Sorting  
            if (!(string.IsNullOrEmpty(parameter.SortColumn) && string.IsNullOrEmpty(parameter.SortColumnDirection)) && isPropertyAvailable)
            {
                entities = entities.OrderBy(parameter.SortColumn + " " + parameter.SortColumnDirection);
            }

            //Search  
            if (!string.IsNullOrEmpty(parameter.SearchParam))
            {
                // var whereConditions = typeof(TEntity).GetProperties()
                //     .Select(propertyInfo => $"{propertyInfo.Name} LIKE '%{parameter.SearchParam}%'")
                //     .ToList();
                //
                // entities = entities.Where(string.Join(" OR ", whereConditions));
                // customerData = customerData.Where("");
            }

            //total number of rows count   
            var recordsTotal = entities.Count();

            //Paging
            var data = entities.Skip(skip).Take(pageSize).ToList();
            var modelResult = data.Select(modelMapping).ToList();

            return new CoreDataTable<TDataTable>
            {
                Data = modelResult,
                Draw = parameter.Draw,
                RecordsFiltered = recordsTotal,
                RecordsTotal = recordsTotal
            };
        }
        
        protected bool IsExist(Expression<Func<TEntity, bool>> predicate)
        {
            var dbSet = Context.Set<TEntity>();

            return dbSet.Any(predicate);
        }

        protected Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate, int limit = -1)
        {
            return SearchAsync(null, predicate, limit);
        }
        
        protected async Task<IEnumerable<TEntity>> SearchAsync(Func<DbSet<TEntity>, IQueryable<TEntity>> extendedQuery, Expression<Func<TEntity, bool>> predicate, int limit = -1)
        {
            var dbSet = Context.Set<TEntity>();
            var entities = extendedQuery != null ? extendedQuery.Invoke(dbSet) : dbSet;
            
            return limit == -1 
                ? await entities.Where(predicate).ToListAsync()
                : await entities.Where(predicate).Take(limit).ToListAsync();
        }
        
        protected async Task<IEnumerable<TResult>> SearchAsync<TResult>(Func<DbSet<TEntity>, IEnumerable<TResult>> extendedQuery)
        {
            var dbSet = Context.Set<TEntity>();

            return extendedQuery.Invoke(dbSet);
        }

        #endregion
    }
}
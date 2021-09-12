using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ImageGram.Core.Application.Services;
using ImageGram.Core.Domain.Commons;
using ImageGram.Core.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImageGram.Core.Infrastructure.DataSources
{
    public class CoreDbContext : IdentityDbContext<AppUserIdentity>
    {
        
        #region Fields

        private readonly ISessionUserService _sessionUserService;

        #endregion
        
        #region Constructors

        public CoreDbContext(DbContextOptions<CoreDbContext> options) 
            : base(options)
        {
        }

        public CoreDbContext(
            DbContextOptions<CoreDbContext> options,
            ISessionUserService sessionUserService)
            : base(options)
        {
            _sessionUserService = sessionUserService;
        }

        #endregion
        
        #region Public Methods

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<CoreEntity>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _sessionUserService.UserId;
                        entry.Entity.CreatedDateTime = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _sessionUserService.UserId;
                        entry.Entity.LastModifiedDateTime = DateTime.UtcNow;
                        break;

                    case EntityState.Detached:
                        break;

                    case EntityState.Unchanged:
                        break;

                    case EntityState.Deleted:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        #endregion

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        #endregion
    }
}
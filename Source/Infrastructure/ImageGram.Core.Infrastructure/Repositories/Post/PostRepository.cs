using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGram.Core.Domain.AggregateRoots.Post;
using ImageGram.Core.Domain.Dtos.Post;
using ImageGram.Core.Infrastructure.DataSources;
using Microsoft.EntityFrameworkCore;

namespace ImageGram.Core.Infrastructure.Repositories.Post
{
    public class PostRepository : BaseRepository<Domain.AggregateRoots.Post.Post>, IPostRepository
    {
        #region Constructors

        public PostRepository(CoreDbContext context) : base(context)
        {
        }

        #endregion

        #region IPostRepository Members

        public Task<IEnumerable<PostWithLatestCommentDto>> GetWithCommentsAsync(int startIndex, int length)
        {
            return SearchAsync(delegate(DbSet<Domain.AggregateRoots.Post.Post> dbSet)
            {
                return dbSet.Skip(startIndex).Take(length)
                    .Include(item => item.Account)
                    .Include(item => item.Comments)
                    .ThenInclude(item => item.Account)
                    .OrderByDescending(item => item.Comments.Count())
                    .AsEnumerable()
                    .Select(item => new PostWithLatestCommentDto
                    {
                        Post = item,
                        Comments = item.Comments.OrderByDescending(comm => comm.CreatedDateTime).Take(2)
                    });
            });
        }

        public async Task DeletePostAsync(int accountId)
        {
            var commentContext = Context.Set<Comment>();
            var comments = commentContext.Where(item => item.AccountId == accountId);
            
            commentContext.RemoveRange(comments);

            await DeleteAsync(item => item.AccountId == accountId);
        }

        #endregion
    }
}
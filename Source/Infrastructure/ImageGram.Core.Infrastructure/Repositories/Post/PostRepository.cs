using ImageGram.Core.Domain.AggregateRoots.Post;
using ImageGram.Core.Infrastructure.DataSources;

namespace ImageGram.Core.Infrastructure.Repositories.Post
{
    public class PostRepository : BaseRepository<Domain.AggregateRoots.Post.Post>, IPostRepository
    {
        #region Constructors

        public PostRepository(CoreDbContext context) : base(context)
        {
        }

        #endregion
        
    }
}
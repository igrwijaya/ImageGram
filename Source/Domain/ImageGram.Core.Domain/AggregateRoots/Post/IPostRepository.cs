using System.Collections.Generic;
using System.Threading.Tasks;
using ImageGram.Core.Domain.Commons;
using ImageGram.Core.Domain.Dtos.Post;

namespace ImageGram.Core.Domain.AggregateRoots.Post
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        #region Public Methods

        Task<IEnumerable<PostWithLatestCommentDto>> GetWithCommentsAsync(int startIndex, int length);

        Task DeletePostAsync(int accountId);

        #endregion
    }
}
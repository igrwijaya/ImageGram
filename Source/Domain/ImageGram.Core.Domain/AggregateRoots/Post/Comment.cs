using ImageGram.Core.Domain.Commons;

namespace ImageGram.Core.Domain.AggregateRoots.Post
{
    public class Comment : CoreEntity, IAggregateRoot
    {
        #region Constructors

        public Comment(string userId, int postId, string content)
        {
            UserId = userId;
            PostId = postId;
            Content = content;
        }

        #endregion

        #region Properties

        public string UserId { get; private set; }
        
        public int PostId { get; private set; }

        public string Content { get; private set; }
        
        #endregion

        #region Entity Relation Properties

        public Post Post { get; set; }

        public Account.Account Account { get; set; }

        #endregion
        
        protected override void EnsureValidState()
        {
            
        }
    }
}
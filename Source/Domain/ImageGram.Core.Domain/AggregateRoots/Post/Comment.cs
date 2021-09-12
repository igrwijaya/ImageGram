using ImageGram.Core.Domain.Commons;

namespace ImageGram.Core.Domain.AggregateRoots.Post
{
    public class Comment : CoreEntity, IAggregateRoot
    {
        #region Constructors

        public Comment(int accountId, int postId, string content)
        {
            AccountId = accountId;
            PostId = postId;
            Content = content;
        }

        #endregion

        #region Properties

        public int AccountId { get; private set; }
        
        public int PostId { get; private set; }

        public string Content { get; private set; }
        
        #endregion

        #region Entity Relation Properties

        public Post Post { get; set; }

        public Account.Account Account { get; set; }

        #endregion
    }
}
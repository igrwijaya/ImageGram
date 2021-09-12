using System.Collections.Generic;
using ImageGram.Core.Domain.Commons;

namespace ImageGram.Core.Domain.AggregateRoots.Post
{
    public class Post : CoreEntity, IAggregateRoot
    {
        #region Constructors

        public Post(string userId, string caption, string image)
        {
            UserId = userId;
            Caption = caption;
            Image = image;
        }

        #endregion

        #region Entity Properties

        public string UserId { get; private set; }

        public string Caption { get; private set; }

        public string Image { get; private set; }

        #endregion

        #region Entity Relation Properties

        public IEnumerable<Comment> Comments { get; set; }

        public Account.Account Account { get; set; }

        #endregion

        protected override void EnsureValidState()
        {
            
        }
    }
}
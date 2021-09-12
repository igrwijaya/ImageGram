using System.Collections.Generic;
using System.Linq;
using ImageGram.Core.Domain.Commons;

namespace ImageGram.Core.Domain.AggregateRoots.Post
{
    public class Post : CoreEntity, IAggregateRoot
    {
        #region Constructors

        public Post(int accountId, string caption)
        {
            AccountId = accountId;
            Caption = caption;
        }

        #endregion

        #region Entity Properties

        public int AccountId { get; private set; }

        public string Caption { get; private set; }

        public string Image { get; private set; }

        #endregion

        #region Entity Relation Properties

        public IEnumerable<Comment> Comments { get; set; }

        public Account.Account Account { get; set; }

        #endregion

        #region Public Methods

        public void AddImage(string image)
        {
            Image = image;
        }

        public void AddComment(int accountId, string content)
        {
            var comment = new Comment(accountId, Id, content);
            if (Comments == null || !Comments.Any())
            {
                Comments = new List<Comment>
                {
                    comment
                };
            }
            else
            {
                var comments = Comments.Append(comment);
                Comments = comments;
            }
        }

        #endregion
    }
}
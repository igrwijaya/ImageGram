using System.Collections.Generic;
using ImageGram.Core.Domain.AggregateRoots.Post;
using ImageGram.Core.Domain.Commons;

namespace ImageGram.Core.Domain.AggregateRoots.Account
{
    public interface IAccount : IAggregateRoot
    {
        #region Properties

        public string Id { get; set; }
        
        public string Name { get; set; }

        #endregion

        #region Entity Relation Properties

        public IEnumerable<Post.Post> Posts { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        #endregion
    }
    
    public class Account : IAccount
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public IEnumerable<Post.Post> Posts { get; set; }
        
        public IEnumerable<Comment> Comments { get; set; }
    }
}
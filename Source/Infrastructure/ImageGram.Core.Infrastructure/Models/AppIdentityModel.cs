using System.Collections.Generic;
using ImageGram.Core.Domain.AggregateRoots.Account;
using ImageGram.Core.Domain.AggregateRoots.Post;
using Microsoft.AspNetCore.Identity;

namespace ImageGram.Core.Infrastructure.Models
{
    public class AppIdentityModel: IdentityUser, IAccount
    {
        #region Properties

        public string Name { get; set; }
        
        public IEnumerable<Post> Posts { get; set; }
        
        public IEnumerable<Comment> Comments { get; set; }

        #endregion
    }
}
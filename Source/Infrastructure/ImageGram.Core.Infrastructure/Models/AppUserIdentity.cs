using ImageGram.Core.Domain.AggregateRoots.Account;
using Microsoft.AspNetCore.Identity;

namespace ImageGram.Core.Infrastructure.Models
{
    public class AppUserIdentity : IdentityUser
    {
        #region Entity Relation Properties

        public int AccountId { get; set; }
        
        public Account Account { get; set; }

        #endregion
    }
}
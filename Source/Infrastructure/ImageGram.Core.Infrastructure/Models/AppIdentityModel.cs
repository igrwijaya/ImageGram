using ImageGram.Core.Domain.AggregateRoots.Account;
using Microsoft.AspNetCore.Identity;

namespace ImageGram.Core.Infrastructure.Models
{
    public class AppIdentityModel: IdentityUser, Account
    {
        #region Properties

        public string Name { get; set; }

        #endregion
    }
}
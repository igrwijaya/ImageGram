using System.Threading.Tasks;
using ImageGram.Core.Domain.AggregateRoots.Account;
using ImageGram.Core.Infrastructure.DataSources;

namespace ImageGram.Core.Infrastructure.Repositories.Common
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        #region Constructors

        public AccountRepository(CoreDbContext context) : base(context)
        {
        }

        #endregion
    }
}
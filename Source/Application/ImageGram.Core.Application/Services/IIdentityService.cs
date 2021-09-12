using System.Threading.Tasks;
using ImageGram.Core.Application.Services.Response;

namespace ImageGram.Core.Application.Services
{
    public interface IIdentityService
    {
        #region Public Methods

        Task<IdentityResponse> CreateAsync(int accountId, string email, string password);

        Task<IdentityResponse> LoginAsync(string email, string password);

        #endregion
    }
}
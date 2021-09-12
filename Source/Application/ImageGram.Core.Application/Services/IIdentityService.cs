using System.Threading.Tasks;
using ImageGram.Core.Application.Services.Response;

namespace ImageGram.Core.Application.Services
{
    public interface IIdentityService
    {
        #region Public Methods

        Task<IdentityResponse> CreateAsync(string name, string email, string password, string role);

        Task<IdentityResponse> LoginAsync(string email, string password, string expectedRole);

        #endregion
    }
}
using System.Threading;
using System.Threading.Tasks;
using ImageGram.Core.Application.Commons;
using ImageGram.Core.Application.Services;
using MediatR;

namespace ImageGram.Core.Application.Domain.Common.Account.Queries.AccountLogin
{
    public class AccountLoginQueryHandler : IRequestHandler<AccountLoginQuery, BaseQueryResult<AccountLoginDto>>
    {
        #region Fields

        private readonly IIdentityService _identityService;

        #endregion

        #region Constructors

        public AccountLoginQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        #endregion

        #region Public Methods

        public async Task<BaseQueryResult<AccountLoginDto>> Handle(AccountLoginQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseQueryResult<AccountLoginDto>
            {
                Data = new AccountLoginDto()
            };

            var loginResponse = await _identityService.LoginAsync(request.Email, request.Password);
            if (!loginResponse.IsSuccess)
            {
                response.AddErrorMessages(loginResponse.ErrorMessages);
                return response;
            }

            response.Data.Token = loginResponse.Token;
            
            return response;
        }

        #endregion
        
    }
}
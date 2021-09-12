using System.Threading;
using System.Threading.Tasks;
using ImageGram.Core.Application.Commons;
using ImageGram.Core.Application.Services;
using MediatR;

namespace ImageGram.Core.Application.Domain.Common.Account.Command.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, BaseCommandResult<string>>
    {
        #region Fields

        private readonly IIdentityService _identityService;

        #endregion

        #region Constructors

        public CreateAccountCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        #endregion

        #region Public Methods

        public async Task<BaseCommandResult<string>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResult<string>();
            var identityResponse = await _identityService.CreateAsync(request.Name, request.Email, request.Password);

            if (!identityResponse.IsSuccess)
            {
                response.AddErrorMessages(identityResponse.ErrorMessages);
                return response;
            }

            response.Data = identityResponse.UserId;

            return response;
        }

        #endregion
        
    }
}
using ImageGram.Core.Application.Commons;
using MediatR;

namespace ImageGram.Core.Application.Domain.Common.Account.Command.CreateAccount
{
    public class CreateAccountCommand : IRequest<BaseCommandResult<string>>
    {
        #region Properties

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        #endregion
    }
}
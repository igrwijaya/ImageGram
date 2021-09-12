using System.Threading.Tasks;
using ImageGram.Core.Application.Commons;
using ImageGram.Core.Application.Domain.Common.Account.Command.CreateAccount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageGram.WebAPI.Controllers
{
    public class AccountController : ImageGramController
    {
        #region Constructors

        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        #endregion

        #region APIs

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<BaseCommandResult<string>>> Register(CreateAccountCommand command)
        {
            return await Mediator.Send(command);
        }

        #endregion
    }
}
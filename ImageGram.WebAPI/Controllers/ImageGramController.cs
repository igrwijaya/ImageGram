using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageGram.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImageGramController : ControllerBase
    {
        #region Constructors

        public ImageGramController(IMediator mediator)
        {
            Mediator = mediator;
        }

        #endregion

        #region Protected Properties

        protected IMediator  Mediator { get; }

        #endregion
    }
}
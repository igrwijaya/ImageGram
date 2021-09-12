namespace ImageGram.Core.Application.Services.Response
{
    public class IdentityResponse : BasicResponse
    {
        #region Properties

        public string UserId { get; set; }

        public string Token { get; set; }

        #endregion
    }
}
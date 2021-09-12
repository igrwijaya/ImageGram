namespace ImageGram.Core.Application.Services
{
    public interface ISessionUserService
    {
        #region Properties

        string UserId { get; }
        
        string UserEmail { get; }

        #endregion

    }
}
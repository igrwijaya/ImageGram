
namespace ImageGram.Core.Application.Services.Response
{
    public class GenericResponse<TData> : BasicResponse
    {

        #region Properties
        
        public TData Data { get; set; }


        #endregion

    }
}
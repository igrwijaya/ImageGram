using System.IO;
using System.Threading.Tasks;
using ImageGram.Core.Application.Services;
using ImageGram.Core.Application.Services.Response;
using Microsoft.Extensions.Configuration;

namespace ImageGram.Core.Infrastructure.Services
{
    public class StorageService : AwsBaseService, IStorageService
    {
        #region Constructors

        public StorageService(IConfiguration configuration) : base(configuration)
        {
        }

        #endregion

        #region IStorageService Members

        public async Task<UploadFileResponse> UploadAsync(string folderNameKey, string fileName, Stream file)
        {
            var response = new UploadFileResponse();
            var uploadResponse = await StoreObjectAsync(folderNameKey, fileName, file);

            if (uploadResponse.IsSuccess)
            {
                response.FileName = uploadResponse.Data;
                return response;
            }
            
            response.AddErrorMessages(uploadResponse.ErrorMessages);

            return response;
        }

        public string GetFileUrl(string fileName)
        {
            return GetUrl(fileName);
        }

        #endregion

    }
}
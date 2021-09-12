using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using ImageGram.Core.Application.Services.Response;
using ImageGram.Core.Constant.Constant;
using Microsoft.Extensions.Configuration;

namespace ImageGram.Core.Infrastructure.Services
{
    public class AwsBaseService
    {
        #region Fields

        private static readonly RegionEndpoint BucketRegion = RegionEndpoint.USEast1;
        private SharedCredentialsFile _sharedCredentialsFile;
        private readonly IConfiguration _configuration;

        #endregion

        #region Constructors

        public AwsBaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Protected Methods

        protected async Task<GenericResponse<string>> StoreObjectAsync(string folderNameKey, string fileName, Stream fileStream)
        {
            var response = new GenericResponse<string>();

            try
            {
                SetupAwsCredential();

                if (!_sharedCredentialsFile.TryGetProfile("source_profile", out var basicProfile) ||
                    !AWSCredentialsFactory.TryGetAWSCredentials(basicProfile, _sharedCredentialsFile, out var awsCredentials))
                {
                    throw new Exception("Storage credential not found!");
                }

                using var client = new AmazonS3Client(awsCredentials, basicProfile.Region);
                if (await AmazonS3Util.DoesS3BucketExistV2Async(client, _configuration[ConfigurationConstant.AwsS3Bucket]) == false)
                {
                    throw new Exception("Storage not exist");
                }

                var putObjectRequest = new PutObjectRequest
                {
                    BucketName = _configuration[ConfigurationConstant.AwsS3Bucket],
                    Key = _configuration[folderNameKey] + "/" + fileName,
                    InputStream = fileStream,
                    CannedACL = S3CannedACL.PublicRead
                };

                await client.PutObjectAsync(putObjectRequest);

                response.Data = putObjectRequest.Key;
            }
            catch (AmazonS3Exception e)
            {
                response.AddErrorMessage(e.Message);
            }
            catch (Exception e)
            {
                response.AddErrorMessage(e.Message);
            }

            return response;
        }

        protected string GetUrl(string fileName)
        {
            return _configuration[ConfigurationConstant.AwsS3Url] + fileName;
        }
        
        protected string GetUrl(string configKey, string fileName)
        {
            return _configuration[configKey] + fileName;
        }

        #endregion
        
        #region Private Methods

        private void SetupAwsCredential()
        {
            _sharedCredentialsFile = new SharedCredentialsFile();
            var sourceProfileOptions = new CredentialProfileOptions
            {
                AccessKey = _configuration[ConfigurationConstant.AwsAccessKey],
                SecretKey = _configuration[ConfigurationConstant.AwsSecretKey]
            };

            var sourceProfile = new CredentialProfile("source_profile", sourceProfileOptions)
            {
                Region = BucketRegion
            };

            _sharedCredentialsFile.RegisterProfile(sourceProfile);
        }

        #endregion
    }
}
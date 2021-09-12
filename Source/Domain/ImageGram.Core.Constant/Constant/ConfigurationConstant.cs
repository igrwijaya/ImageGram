namespace ImageGram.Core.Constant.Constant
{
    public class ConfigurationConstant
    {
        #region General

        public const string ConnMysql = "Connection:MySql";

        #endregion
        
        #region JWT

        public const string JwtAudience = "Jwt:ValidAudience";
        public const string JwtIssuer = "Jwt:ValidIssuer";
        public const string JwtKey = "Jwt:Secret";

        #endregion
        
        #region AWS
        
        public const string AwsAccessKey = "AWS:AccessKey";
        public const string AwsSecretKey = "AWS:SecretKey";
        public const string AwsS3Url = "AWS:PublicS3Url";
        public const string AwsS3Bucket = "AWS:S3Bucket:Root";

        #endregion
    }
}
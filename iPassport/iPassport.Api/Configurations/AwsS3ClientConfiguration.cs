using Amazon;
using Amazon.S3;
using iPassport.Application.Services.Constants;

namespace iPassport.Api.Configurations
{
    /// <summary>
    /// Aws S3 Configuration Class
    /// </summary>
    public static class AwsS3ClientConfiguration
    {
        /// <summary>
        /// Aws S3 client Setup
        /// </summary>
        /// <returns>AmazonS3Client isntance</returns>
        public static AmazonS3Client AwsS3ClientSetup()
        {
            string awsAccesskey = EnvConstants.AWS_ACCESS_KEY_ID;
            string awsSecret = EnvConstants.AWS_SECRET_ACCESS_KEY;
            RegionEndpoint region = RegionEndpoint.GetBySystemName(EnvConstants.AWS_DEFAULT_REGION);

            return new AmazonS3Client(awsAccesskey, awsSecret, region);
        }
    }
}

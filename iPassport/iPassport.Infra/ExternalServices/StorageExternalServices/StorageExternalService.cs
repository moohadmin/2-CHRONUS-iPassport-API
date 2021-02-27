using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace iPassport.Infra.ExternalServices.StorageExternalServices
{
    public class StorageExternalService : IStorageExternalService
    {
        private IAmazonS3 _awsS3;
        private readonly StorageConfigurations _settings;

        public StorageExternalService(IAmazonS3 awsS3, IOptions<StorageConfigurations> settings)
        {
            _awsS3 = awsS3;
            _settings = settings.Value;
        }

        public async Task<string> UploadFileAsync(IFormFile imageFile, string fileName)
        {
            using var memoryStream = new MemoryStream();
            await imageFile.CopyToAsync(memoryStream);

            // Upload the file if less than 2 MB
            if (memoryStream.Length > 2097152)
                throw new BusinessException("The file is too large... Max: (2Mb)");

            await UpToS3Async(memoryStream, fileName);

            var uri = $"https://{_settings.BucketName}.s3.{RegionEndpoint.EUWest2}.amazonaws.com/{fileName}";

            return uri;
        }

        public async Task<ListVersionsResponse> FilesList()
        {
            return await _awsS3.ListVersionsAsync(_settings.BucketName);
        }

        public async Task<Stream> GetFile(string key)
        {
            GetObjectResponse response = await _awsS3.GetObjectAsync(_settings.BucketName, key);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                throw new BusinessException("File not found");

            return response.ResponseStream;
        }

        private async Task<bool> UpToS3Async(Stream FileStream, string filename)
        {
            _awsS3 = new AmazonS3Client(_settings.awsAccesskey, _settings.awsSecret, RegionEndpoint.SAEast1);
            try
            {
                PutObjectRequest request = new PutObjectRequest()
                {
                    InputStream = FileStream,
                    BucketName = _settings.BucketName,
                    Key = filename
                };
                PutObjectResponse response = await _awsS3.PutObjectAsync(request);
                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw new PersistenceException(ex);
            }
        }
    }
}
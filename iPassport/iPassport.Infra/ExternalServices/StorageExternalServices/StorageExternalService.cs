using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace iPassport.Infra.ExternalServices.StorageExternalServices
{
    public class StorageExternalService : IStorageExternalService
    {
        private readonly IAmazonS3 _awsS3;
        private readonly StorageConfigurations _settings;

        public StorageExternalService(IOptions<StorageConfigurations> settings)
        {
            _settings = settings.Value;
            _awsS3 = new AmazonS3Client(_settings.awsAccesskey, _settings.awsSecret, RegionEndpoint.SAEast1);
        }

        public async Task<string> UploadFileAsync(IFormFile imageFile, string fileName)
        {
            using var memoryStream = new MemoryStream();
            await imageFile.CopyToAsync(memoryStream);

            // Upload the file if less than 2 MB
            if (memoryStream.Length > 10000000)
                throw new BusinessException("The file is too large... Max: (2Mb)");

            if (!await UpToS3Async(memoryStream, fileName))
                throw new BusinessException("Arquivo não pode ser gravado");

            return fileName;
        }

        public async Task<Amazon.S3.Model.ListVersionsResponse> FilesList()
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

        public string GeneratePreSignedURL(string filename)
        {
            if (String.IsNullOrWhiteSpace(filename))
                throw new BusinessException("O usuário não tem foto cadastrada");

            try
            {
                GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
                {
                    BucketName = _settings.BucketName,
                    Key = filename,
                    Expires = DateTime.UtcNow.AddHours(1)
                };
                return  _awsS3.GetPreSignedURL(request1);
            }
            catch (AmazonS3Exception e)
            {
                throw new PersistenceException("Error encountered on server. Message:'{0}' when writing an object", e);
            }
            catch (Exception e)
            {
                throw new PersistenceException("Unknown encountered on server. Message:'{0}' when writing an object", e);
            }
        }
    }
}
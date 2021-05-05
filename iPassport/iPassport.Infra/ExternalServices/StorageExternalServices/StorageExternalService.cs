using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Resources;
using iPassport.Application.Services.Constants;
using iPassport.Domain.Enums;
using iPassport.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace iPassport.Infra.ExternalServices.StorageExternalServices
{
    public class StorageExternalService : IStorageExternalService
    {
        private readonly IAmazonS3 _awsS3;
        private readonly string _bucketName;
        private readonly IStringLocalizer<Resource> _localizer;

        public StorageExternalService(IStringLocalizer<Resource> localizer)
        {
            string awsAccesskey = EnvConstants.AWS_ACCESS_KEY_ID;
            string awsSecret = EnvConstants.AWS_SECRET_ACCESS_KEY;
            RegionEndpoint region = RegionEndpoint.GetBySystemName(EnvConstants.AWS_DEFAULT_REGION);

            _bucketName = EnvConstants.STORAGE_S3_BUCKET_NAME;
            _awsS3 = new AmazonS3Client(awsAccesskey, awsSecret, region);
            _localizer = localizer;
        }

        public async Task<string> UploadFileAsync(IFormFile imageFile, string fileName)
        {
            var memoryStream = new MemoryStream();

            try
            {
                foreach (EImageSize size in Enum.GetValues(typeof(EImageSize)))
                {
                    memoryStream = await ResiseImage(imageFile, size);
                    string path = GetFilePath(size, fileName);
                    await UpToS3Async(memoryStream, path);
                }
            }
            finally
            {
                memoryStream.Dispose();
            }

            return fileName;
        }

        public async Task<ListVersionsResponse> FilesList()
        {
            return await _awsS3.ListVersionsAsync(_bucketName);
        }

        public async Task<Stream> GetFile(string key)
        {
            GetObjectResponse response = await _awsS3.GetObjectAsync(_bucketName, key);

            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new BusinessException(_localizer["FileNotFound"]);

            return response.ResponseStream;
        }

        public string GeneratePreSignedURL(string filename, EImageSize? size)
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new BusinessException(_localizer["UserNotHavePhoto"]);

            try
            {
                GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
                {
                    BucketName = _bucketName,
                    Key = GetFilePath(size, filename),
                    Expires = DateTime.UtcNow.AddHours(1)
                };

                return _awsS3.GetPreSignedURL(request);
            }
            catch (Exception)
            {
                throw new BusinessException(_localizer["OperationNotPerformed"]);
            }
        }

        private async Task<bool> UpToS3Async(Stream FileStream, string path)
        {
            try
            {
                PutObjectRequest request = new PutObjectRequest()
                {
                    InputStream = FileStream,
                    BucketName = _bucketName,
                    Key = $"{path}"
                };

                PutObjectResponse response = await _awsS3.PutObjectAsync(request);
                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw new PersistenceException(_localizer["OperationNotPerformed"], e);
            }
        }

        private async Task<MemoryStream> ResiseImage(IFormFile formFile, EImageSize imageSize)
        {
            try
            {
                using var imageStream = new MemoryStream();

                await formFile.CopyToAsync(imageStream);

                using var img = Image.FromStream(imageStream);
                using var image = new Bitmap(img);
                var imgReturn = new Bitmap((int)imageSize, (int)imageSize);

                using (var graphic = Graphics.FromImage(imgReturn))
                {
                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphic.SmoothingMode = SmoothingMode.HighQuality;
                    graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphic.CompositingQuality = CompositingQuality.HighQuality;
                    graphic.DrawImage(image, 0, 0, (int)imageSize, (int)imageSize);
                }

                var outputStream = new MemoryStream();
                imgReturn.Save(outputStream, ImageFormat.Jpeg);
                outputStream.Seek(0, SeekOrigin.Begin);

                return outputStream;

            }
            catch (Exception e)
            {
                throw new InternalErrorException(e.Message, (int)HttpStatusCode.InternalServerError, e.StackTrace);
            }
        }

        private static string GetFilePath(EImageSize? size, string filename)
        {
            size = size == null ? EImageSize.medium : size;

            return $"{Constants.S3_USER_IMAGES_PATH}/{size}/{filename}";
        }
    }
}
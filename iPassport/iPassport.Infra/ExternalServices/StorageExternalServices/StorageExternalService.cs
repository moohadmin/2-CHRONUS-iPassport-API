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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace iPassport.Infra.ExternalServices.StorageExternalServices
{
    public class StorageExternalService : IStorageExternalService
    {
        private readonly AmazonS3Client _awsS3;
        private readonly string _bucketName;
        private readonly IStringLocalizer<Resource> _localizer;

        public StorageExternalService(IStringLocalizer<Resource> localizer, AmazonS3Client awsS3)
        {
            _awsS3 = awsS3;
            _localizer = localizer;
            _bucketName = EnvConstants.STORAGE_S3_BUCKET_NAME;
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
        public async Task DeleteFileAsync(string fileName)
        {
            var fileList = new List<KeyVersion>();
            var keyList = new List<string>();

            foreach (EImageSize size in Enum.GetValues(typeof(EImageSize)))
            {
                string path = await GetKey(size, fileName);
                keyList.Add(path);
            }
            fileList = keyList.Distinct().Select(x => new KeyVersion() { Key = x }).ToList();

            await DeleteFromS3Async(fileList);
        }

        public async Task<string> GeneratePreSignedURL(string filename, EImageSize? size)
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new BusinessException(_localizer["UserNotHavePhoto"]);

            try
            {
                GetPreSignedUrlRequest request = new()
                {
                    BucketName = _bucketName,
                    Key = await GetKey(size, filename),
                    Expires = DateTime.UtcNow.AddHours(1)
                };

                return _awsS3.GetPreSignedURL(request);
            }
            catch (Exception e)
            {
                throw new InternalErrorException(e.Message, (int)HttpStatusCode.InternalServerError, e.StackTrace);
            }
        }

        private async Task<bool> UpToS3Async(Stream FileStream, string path)
        {
            try
            {
                PutObjectRequest request = new()
                {
                    InputStream = FileStream,
                    BucketName = _bucketName,
                    Key = $"{path}"
                };

                PutObjectResponse response = await _awsS3.PutObjectAsync(request);
                if (response.HttpStatusCode != HttpStatusCode.OK)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw new PersistenceException(_localizer["OperationNotPerformed"], e);
            }
        }

        private async Task<bool> DeleteFromS3Async(List<KeyVersion> fileList)
        {
            try
            {
                DeleteObjectsRequest request = new DeleteObjectsRequest
                {
                    BucketName = _bucketName,
                    Objects = fileList
                };
                DeleteObjectsResponse response = await _awsS3.DeleteObjectsAsync(request);
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

        private async Task<string> GetKey(EImageSize? size, string filename)
        {
            var filepath = GetFilePath(size, filename);

            if (await VerifyKey(filepath)) return filepath;

            return filename;
        }

        private async Task<bool> VerifyKey(string key)
        {
            try
            {
                var image = await _awsS3.GetObjectAsync(_bucketName, key);
                return image != null;
            }
            catch (AmazonS3Exception aS3Ex)
            {
                return !(aS3Ex.StatusCode == HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                if (e.InnerException.GetType() == typeof(AmazonS3Exception) && 
                    ((AmazonS3Exception)e.InnerException).StatusCode == HttpStatusCode.NotFound) {
                        return false;
                }
                throw new InternalErrorException(e.Message, (int)HttpStatusCode.InternalServerError, e.StackTrace);
            }
        }

        private static string GetFilePath(EImageSize? size, string filename)
        {
            size = size == null ? EImageSize.medium : size;

            // return $"{Constants.S3_USER_IMAGES_PATH}/{size}/{filename}";
            return $"{filename}";
        }
    }
}
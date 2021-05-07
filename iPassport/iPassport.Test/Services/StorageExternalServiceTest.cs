using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using iPassport.Application.Interfaces;
using iPassport.Application.Resources;
using iPassport.Domain.Enums;
using iPassport.Infra.ExternalServices.StorageExternalServices;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class StorageExternalServiceTest
    {
        Mock<IStringLocalizer<Resource>> _mockLocalizer;
        Mock<AmazonS3Client> _mockS3Client;
        IStorageExternalService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockS3Client = new Mock<AmazonS3Client>(FallbackCredentialsFactory.GetCredentials(true), new AmazonS3Config { RegionEndpoint = RegionEndpoint.SAEast1 });
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();

            EnvVariablesFactory.Create();

            _service = new StorageExternalService(_mockLocalizer.Object, _mockS3Client.Object);
        }

        [TestMethod]
        public void UploadFileAsyncTest()
        {
            var filename = $"{Guid.NewGuid()}.jpg";
            
            //Arrange
            _mockS3Client.Setup(x => x.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()).Result)
                .Returns(new PutObjectResponse() { HttpStatusCode = HttpStatusCode.OK });

            //Act
            var result = _service.UploadFileAsync(GetIFormFile(), filename);

            // Assert
            _mockS3Client.Verify(a => a.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()), Times.Exactly(3));
            Assert.IsInstanceOfType(result, typeof(Task<string>));
            Assert.IsNotNull(result.Result);
            Assert.AreEqual(filename, result.Result);
        }

        [TestMethod]
        public void RemoveFileAsyncTest()
        {
            var filename = $"{Guid.NewGuid()}.jpg";

            //Arrange
            _mockS3Client.Setup(x => x.DeleteObjectsAsync(It.IsAny<DeleteObjectsRequest>(), It.IsAny<CancellationToken>()).Result)
                .Returns(new DeleteObjectsResponse() { HttpStatusCode = HttpStatusCode.OK });

            //Act
           _service.DeleteFileAsync(filename);

            // Assert
            _mockS3Client.Verify(a => a.DeleteObjectsAsync(It.IsAny<DeleteObjectsRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        private static IFormFile GetIFormFile()
        {
            var imageStream = new MemoryStream(GenerateImageByteArray());
            var image = new FormFile(imageStream, 0, imageStream.Length, "UnitTest", "UnitTest.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            return image;
        }

        private static byte[] GenerateImageByteArray(int width = 50, int height = 50)
        {
            Bitmap bitmapImage = new Bitmap(width, height);
            Graphics imageData = Graphics.FromImage(bitmapImage);
            imageData.DrawLine(new Pen(Color.Blue), 0, 0, width, height);

            MemoryStream memoryStream = new MemoryStream();
            byte[] byteArray;

            using (memoryStream)
            {
                bitmapImage.Save(memoryStream, ImageFormat.Jpeg);
                byteArray = memoryStream.ToArray();
            }
            return byteArray;
        }
    }
}

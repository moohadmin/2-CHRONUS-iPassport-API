using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Api.Models.Requests;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;


namespace iPassport.Test.Controllers
{
    [TestClass()]
    public class PassportControllerTest
    {
        Mock<IPassportService> _mockService;
        IMapper _mapper;
        PassportController _controller;
        PassportUseCreateRequest _requestAccess;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IPassportService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new PassportController(_mockService.Object, _mapper);
            _requestAccess = new PassportUseCreateRequest()
            {
                Latitude = -80,
                Longitude = 100,
                PassportDetailsId = Guid.NewGuid()
            };
        }


        [TestMethod]
        public void Get_MustReturnOk()
        {
            var seed = PassportSeed.Get();

            // Arrange
            _mockService.Setup(r => r.Get()).Returns(Task.FromResult(new ResponseApi(true, "Test Success!", seed)));

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void AccessApproved_MustReturnOk()
        {
            var mockRequest = _requestAccess;

            // Arrange
            _mockService.Setup(r => r.AddAccessApproved(It.IsNotNull<PassportUseCreateDto>())).Returns(Task.FromResult(new ResponseApi(true, "Acesso Aprovado")));

            // Act
            var result = _controller.AccessApproved(mockRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void AcessDenied_MustReturnOk()
        {
            var mockRequest = _requestAccess;

            // Arrange
            _mockService.Setup(r => r.Get()).Returns(Task.FromResult(new ResponseApi(true, "Acesso Recusado")));

            // Act
            var result = _controller.AcessDenied(mockRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}

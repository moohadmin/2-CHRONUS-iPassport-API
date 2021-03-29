using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Api.Models.Requests;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
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
    public class ImportedFileControllerTest
    {
        Mock<IImportedFileService> _mockService;
        IMapper _mapper;
        ImportedFileController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IImportedFileService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new ImportedFileController(_mapper, _mockService.Object);
            
        }
        
        [TestMethod]
        public void GetByPeriod_MustReturnOk()
        {
            var mockRequest = Mock.Of<GetImportedFileRequest>();

            // Arrange
            _mockService.Setup(r => r.FindByPeriod(It.IsAny<GetImportedFileFilter>()));

            // Act
            var result = _controller.GetByPeriod(mockRequest);

            // Assert
            _mockService.Verify(a => a.FindByPeriod(It.IsAny<GetImportedFileFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetById_MustReturnOk()
        {
            // Arrange
            _mockService.Setup(r => r.GetImportedFileDetails(It.IsAny<Guid>()));

            // Act
            var result = _controller.GetById(It.IsAny<Guid>());

            // Assert
            _mockService.Verify(a => a.GetImportedFileDetails(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

    }
}

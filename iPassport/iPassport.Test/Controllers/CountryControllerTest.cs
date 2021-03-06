using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.Shared;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace iPassport.Test.Controllers
{
    [TestClass]
    public class CountryControllerTest
    {
        Mock<ICountryService> _mockService;
        IMapper _mapper;
        CountryController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<ICountryService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new CountryController(_mapper, _mockService.Object);
        }

        [TestMethod]
        public void Get_MustReturnOk()
        {
            var mockRequest = Guid.NewGuid();

            // Arrange
            _mockService.Setup(r => r.FindById(It.IsAny<Guid>()));

            // Act
            var result = _controller.Get(mockRequest);

            // Assert
            _mockService.Verify(a => a.FindById(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetByNameParts_MustReturnOk()
        {
            var mockRequest = Mock.Of<GetByNamePartsPagedRequest>();

            // Arrange
            _mockService.Setup(r => r.FindByNameParts(It.IsAny<GetByNamePartsPagedFilter>()));

            // Act
            var result = _controller.GetByNameParts(mockRequest);

            // Assert
            _mockService.Verify(a => a.FindByNameParts(It.IsAny<GetByNamePartsPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Add_MustReturnOk()
        {
            var mockRequest = Mock.Of<CountryCreateRequest>();

            // Arrange
            _mockService.Setup(r => r.Add(It.IsAny<CountryCreateDto>()));

            // Act
            var result = _controller.Add(mockRequest);

            // Assert
            _mockService.Verify(a => a.Add(It.IsAny<CountryCreateDto>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}

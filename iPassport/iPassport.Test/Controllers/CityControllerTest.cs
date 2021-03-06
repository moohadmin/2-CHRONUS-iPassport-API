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
    public class CityControllerTest
    {
        Mock<ICityService> _mockService;
        IMapper _mapper;
        CityController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<ICityService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new CityController(_mapper, _mockService.Object);
        }

        [TestMethod]
        public void GetByStateAndNameParts_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<GetPagedCitiesByStateAndNamePartsRequest>();
            _mockService.Setup(r => r.FindByStateAndNameParts(It.IsAny<GetByIdAndNamePartsPagedFilter>()));

            // Act
            var result = _controller.GetByStateAndNameParts(mockRequest);

            // Assert
            _mockService.Verify(a => a.FindByStateAndNameParts(It.IsAny<GetByIdAndNamePartsPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        
    }
}

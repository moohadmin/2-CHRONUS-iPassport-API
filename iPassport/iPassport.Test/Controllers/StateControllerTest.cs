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
    public class StateControllerTest
    {
        Mock<IStateService> _mockService;
        IMapper _mapper;
        StateController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IStateService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new StateController(_mapper, _mockService.Object);
        }

        [TestMethod]
        public void GetByCountryId_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<GetPagedStatesByCountryRequest>();
            _mockService.Setup(r => r.GetByCountryId(It.IsAny<GetByIdPagedFilter>()));

            // Act
            var result = _controller.GetByCountryId(mockRequest);

            // Assert
            _mockService.Verify(a => a.GetByCountryId(It.IsAny<GetByIdPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Add_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<StateCreateRequest>();
            _mockService.Setup(r => r.Add(It.IsAny<StateCreateDto>()));

            // Act
            var result = _controller.Add(mockRequest);

            // Assert
            _mockService.Verify(a => a.Add(It.IsAny<StateCreateDto>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}

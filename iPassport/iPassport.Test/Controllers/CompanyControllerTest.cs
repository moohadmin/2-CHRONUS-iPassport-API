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
    public class CompanyControllerTest
    {
        Mock<ICompanyService> _mockService;
        IMapper _mapper;
        CompanyController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<ICompanyService>();
            _mapper = AutoMapperFactory.Create();
            _controller = new CompanyController(_mapper, _mockService.Object);
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
            var mockRequest = Mock.Of<CompanyCreateRequest>();

            // Arrange
            _mockService.Setup(r => r.Add(It.IsAny<CompanyCreateDto>()));

            // Act
            var result = _controller.Add(mockRequest);

            // Assert
            _mockService.Verify(a => a.Add(It.IsAny<CompanyCreateDto>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}

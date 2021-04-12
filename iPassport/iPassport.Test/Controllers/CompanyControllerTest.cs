using AutoMapper;
using iPassport.Api.Controllers;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.Company;
using iPassport.Api.Models.Requests.Shared;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
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
            var mockRequest = Mock.Of<GetCompaniesPagedRequest>();

            // Arrange
            _mockService.Setup(r => r.FindByNameParts(It.IsAny<GetCompaniesPagedFilter>()));

            // Act
            var result = _controller.GetByNameParts(mockRequest);

            // Assert
            _mockService.Verify(a => a.FindByNameParts(It.IsAny<GetCompaniesPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        //[TestMethod]
        //public void Add_MustReturnOk()
        //{
        //    var mockRequest = Mock.Of<CompanyCreateRequest>();

        //    // Arrange
        //    _mockService.Setup(r => r.Add(It.IsAny<CompanyCreateDto>()));

        //    // Act
        //    var result = _controller.Add(mockRequest);

        //    // Assert
        //    _mockService.Verify(a => a.Add(It.IsAny<CompanyCreateDto>()));
        //    Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
        //    Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        //}

        [TestMethod]
        public void GetById_MustReturnOk()
        {
            var mockRequest = Guid.NewGuid();

            // Arrange
            _mockService.Setup(r => r.GetById(It.IsAny<Guid>()).Result)
                .Returns(new ResponseApi(true, "test", CompanySeed.Get()));

            // Act
            var result = _controller.GetById(mockRequest);

            // Assert
            _mockService.Verify(a => a.GetById(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetAllTypes_MustReturnOk()
        {
            // Arrange
            _mockService.Setup(r => r.GetAllTypes()).Returns(Task.FromResult(new ResponseApi(true, "Test Success!", null)));

            // Act
            var result = _controller.GetAllTypes();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetPagedSegmetsByTypeId_MustReturnOk()
        {
            // Arrange
            var typeId = Guid.NewGuid();
            var filter = Mock.Of<PageFilter>();
            _mockService.Setup(r => r.GetSegmetsByTypeId(typeId, filter).Result)
                .Returns(new PagedResponseApi(true, "Sucess", 1, 10, 1, 10, CompanySegmentSeed.GetAll()));

            // Act
            var result = _controller.GetAllTypes();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetHeadquartersCompanies_MustReturnOk()
        {
            var mockRequest = Mock.Of<GetHeadquarterCompanyRequest>();

            // Arrange
            _mockService.Setup(r => r.GetHeadquartersCompanies(It.IsAny<GetHeadquarterCompanyFilter>()))
                .Returns(Task.FromResult(new ResponseApi(true, "Test Success!", _mapper.Map<IList<HeadquarterCompanyViewModel>>(CompanySeed.GetCompanies()))));

            // Act
            var result = _controller.GetHeadquartersCompanies(mockRequest);

            // Assert
            _mockService.Verify(a => a.GetHeadquartersCompanies(It.IsAny<GetHeadquarterCompanyFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}

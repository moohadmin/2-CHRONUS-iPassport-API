using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using iPassport.Test.Settings.Seeds;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class CompanyServiceTest
    {
        Mock<ICompanyRepository> _mockRepository;
        ICompanyService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;
        Mock<ICityRepository> _mockCityRepository;
        Mock<ICompanyTypeRepository>  _mockCompanyTypeRepository;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<ICompanyRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
            _mockCityRepository = new Mock<ICityRepository>();
            _mockCompanyTypeRepository = new Mock<ICompanyTypeRepository>();

            _service = new CompanyService(_mockRepository.Object, _mockLocalizer.Object, _mapper, _mockCityRepository.Object, _mockCompanyTypeRepository.Object);
        }
        
        [TestMethod]
        public void FindByNameParts_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<GetByNamePartsPagedFilter>();
            _mockRepository.Setup(x => x.FindByNameParts(It.IsAny<GetByNamePartsPagedFilter>()).Result).Returns(CompanySeed.GetPaged());

            // Act
            var result = _service.FindByNameParts(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.FindByNameParts(It.IsAny<GetByNamePartsPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void Add_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.AddressDto == Mock.Of<AddressCreateDto>());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());

            // Act
            var result = _service.Add(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.InsertAsync(It.IsAny<Company>()));
            _mockCityRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetById_MustReturnOk()
        {
            // Arrange
            var mockRequest = Guid.NewGuid();
            _mockRepository.Setup(x => x.GetLoadedCompanyById(It.IsAny<Guid>()).Result).Returns(CompanySeed.Get());

            // Act
            var result = _service.GetById(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.GetLoadedCompanyById(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetAllTypes_MustReturnOk()
        {
            // Arrange
            _mockCompanyTypeRepository.Setup(r => r.FindAll().Result).Returns(CompanyTypeSeed.GetCompanyTypes());

            // Act
            var result = _service.GetAllTypes();

            // Assert
            _mockCompanyTypeRepository.Verify(a => a.FindAll(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsInstanceOfType(result.Result.Data, typeof(IList<CompanyTypeViewModel>));
        }
    }
}

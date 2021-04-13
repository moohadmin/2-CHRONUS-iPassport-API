using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using iPassport.Test.Settings.Seeds;
using Microsoft.AspNetCore.Http;
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
        IStringLocalizer<Resource> _localizer;
        Mock<ICityRepository> _mockCityRepository;
        Mock<ICompanyTypeRepository> _mockCompanyTypeRepository;
        Mock<ICompanySegmentRepository> _mockCompanySegmentRepository;
        Resource _resource;
        IHttpContextAccessor _accessor;
        
        [TestInitialize]
        public void Setup()
        {
            _resource = ResourceFactory.Create();
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<ICompanyRepository>();
            _localizer = ResourceFactory.GetStringLocalizer();
            _mockCityRepository = new Mock<ICityRepository>();
            _mockCompanyTypeRepository = new Mock<ICompanyTypeRepository>();
            _mockCompanySegmentRepository = new Mock<ICompanySegmentRepository>();
            _accessor = HttpContextAccessorFactory.Create();
            _service = new CompanyService(_mockRepository.Object, _localizer, _mapper, _mockCityRepository.Object, _mockCompanyTypeRepository.Object, _mockCompanySegmentRepository.Object, _accessor);
            
        }

        [TestMethod]
        public void FindByNameParts_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<GetCompaniesPagedFilter>();
            _mockRepository.Setup(x => x.FindByNameParts(It.IsAny<GetCompaniesPagedFilter>()).Result).Returns(CompanySeed.GetPaged());

            // Act
            var result = _service.FindByNameParts(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.FindByNameParts(It.IsAny<GetCompaniesPagedFilter>()));
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
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

        [TestMethod]
        public void GetSegmetsByTypeId_MustReturnOk()
        {
            // Arrange
            var typeId = Guid.NewGuid();
            var filter = Mock.Of<PageFilter>();
            _mockCompanySegmentRepository.Setup(r => r.GetPagedByTypeId(typeId, filter).Result).Returns(CompanySegmentSeed.GetPaged());

            // Act
            var result = _service.GetSegmetsByTypeId(typeId, filter);

            // Assert
            _mockCompanySegmentRepository.Verify(a => a.GetPagedByTypeId(typeId, filter), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsInstanceOfType(result.Result.Data, typeof(IList<CompanySegmentViewModel>));
        }

        [TestMethod]
        public void GetHeadquartersCompanies_Private()
        {
            var mockRequest = Mock.Of<GetHeadquarterCompanyFilter>(x => x.CompanyTypeId == Guid.NewGuid() && x.Cnpj == "00000000" && x.SegmentId == Guid.NewGuid());

            // Arrange
            _mockCompanyTypeRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(new CompanyType("test", (int)ECompanyType.Private));
            _mockCompanySegmentRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(new CompanySegment("test", (int)ECompanySegmentType.Contractor, Guid.NewGuid()));
            _mockRepository.Setup(x => x.GetPrivateHeadquarters(It.IsAny<string>(), It.IsAny<int>()).Result).Returns(CompanySeed.GetCompanies());

            // Act
            var result = _service.GetHeadquartersCompanies(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.GetPrivateHeadquarters(It.IsAny<string>(), It.IsAny<int>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetHeadquartersCompanies_GovermentMunicipal()
        {
            var mockRequest = Mock.Of<GetHeadquarterCompanyFilter>(x => x.CompanyTypeId == Guid.NewGuid() && x.SegmentId == Guid.NewGuid() && x.LocalityId == Guid.NewGuid());

            // Arrange
            _mockCompanyTypeRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(new CompanyType("test", (int)ECompanyType.Government));
            _mockCompanySegmentRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(new CompanySegment("test", (int)ECompanySegmentType.Municipal, Guid.NewGuid()));
            _mockRepository.Setup(x => x.GetPublicMunicipalHeadquarters(It.IsAny<Guid>()).Result).Returns(CompanySeed.GetCompanies());

            // Act
            var result = _service.GetHeadquartersCompanies(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.GetPublicMunicipalHeadquarters(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetHeadquartersCompanies_GovermentState()
        {
            var mockRequest = Mock.Of<GetHeadquarterCompanyFilter>(x => x.CompanyTypeId == Guid.NewGuid() && x.SegmentId == Guid.NewGuid() && x.LocalityId == Guid.NewGuid());

            // Arrange
            _mockCompanyTypeRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(new CompanyType("test", (int)ECompanyType.Government));
            _mockCompanySegmentRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(new CompanySegment("test", (int)ECompanySegmentType.State, Guid.NewGuid()));

            _mockRepository.Setup(x => x.GetPublicStateHeadquarters(It.IsAny<Guid>()).Result).Returns(CompanySeed.GetCompanies());

            // Act
            var result = _service.GetHeadquartersCompanies(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.GetPublicStateHeadquarters(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("A1234567")]
        [DataRow("1234567")]
        public void GetHeadquartersCompanies_CnpjInvalid(string cnpj)
        {
            var mockRequest = Mock.Of<GetHeadquarterCompanyFilter>(x => x.CompanyTypeId == Guid.NewGuid() && x.Cnpj == cnpj && x.SegmentId == Guid.NewGuid());

            // Arrange
            _mockCompanyTypeRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result)
                .Returns(new CompanyType("test", (int)ECompanyType.Private));
            
            _mockCompanySegmentRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result)
                .Returns(new CompanySegment("test", (int)ECompanySegmentType.Contractor, Guid.NewGuid()));

            // Assert
            Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.GetHeadquartersCompanies(mockRequest),
                _resource.GetMessage("CnpjRequiredForPrivateCompany"));
        }

        [TestMethod]
        public void GetHeadquartersCompanies_LocationIdNull()
        {
            var mockRequest = Mock.Of<GetHeadquarterCompanyFilter>(x => x.CompanyTypeId == Guid.NewGuid() && x.SegmentId == Guid.NewGuid() && x.LocalityId == null);

            // Arrange
            _mockCompanyTypeRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result)
                .Returns(new CompanyType("test", (int)ECompanyType.Government));
            
            _mockCompanySegmentRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result)
                .Returns(new CompanySegment("test", (int)ECompanySegmentType.State, Guid.NewGuid()));

            // Assert
            Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.GetHeadquartersCompanies(mockRequest),
                string.Format(_resource.GetMessage("RequiredField"), _resource.GetMessage("Locality")));
        }


        #region AddMethod
        [TestMethod]
        public void Add_MustHaveIsActiveValue()
        {
            //Arrage
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.IsActive == null);

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["RequiredField"], _localizer["IsActive"]), ex.Message);
        }
        [TestMethod]
        public void Add_MustHaveAddress()
        {
            //Arrage
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.IsActive == false);

            //Assert
            var ex =  Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
           Assert.AreEqual(string.Format(_localizer["RequiredField"], _localizer["Address"]), ex.Message);
        }
        [TestMethod]
        public void Add_MustFoundCity()
        {
            //Arrage
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.IsActive == false && x.Address == Mock.Of<AddressCreateDto>());

            //Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(_localizer["CityNotFound"], ex.Message);
            _mockCityRepository.Verify(x => x.Find(It.IsAny<Guid>()));
        }
        [TestMethod]
        public void Add_MustFoundSegment()
        {
            //Arrage
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.IsActive == false && x.Address == Mock.Of<AddressCreateDto>());
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());

            //Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(_localizer["SegmentNotFound"], ex.Message);
            _mockCityRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockCompanySegmentRepository.Verify(x => x.GetLoaded(It.IsAny<Guid>()));
        }
        [TestMethod]
        public void Add_PrivateType_SegmentHealth_HeadQuarter_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == true && x.ParentId == null);
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(CompanySegmentSeed.GetHealthType());
            _mockRepository.Setup(x => x.GetLoadedCompanyById(It.IsAny<Guid>()).Result).Returns(CompanySeed.GetHealthType());

            // Act
            var result = _service.Add(mockRequest);

            // Assert
            _mockCityRepository.Verify(x => x.Find(It.IsAny<Guid>()));
            _mockRepository.Verify(x => x.InsertAsync(It.IsAny<Company>()));
            _mockCompanySegmentRepository.Verify(x => x.GetLoaded(It.IsAny<Guid>()));
            _mockRepository.Verify(x => x.GetLoadedCompanyById(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.AreEqual(true, result.Result.Success);
            Assert.IsInstanceOfType(result.Result.Data, typeof(CompanyCreateResponseViewModel));
        }
        #endregion


    }
}

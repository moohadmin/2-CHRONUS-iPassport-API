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
        Mock<IStateRepository> _mockStateRepository;
        Mock<IAddressRepository> _mockAddressRepository;
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
            _mockStateRepository = new Mock<IStateRepository>();
            _mockAddressRepository = new Mock<IAddressRepository>();
            _mockCompanyTypeRepository = new Mock<ICompanyTypeRepository>();
            _mockCompanySegmentRepository = new Mock<ICompanySegmentRepository>();
            _accessor = HttpContextAccessorFactory.Create();
            _service = new CompanyService(_mockRepository.Object, _localizer, _mapper, _mockCityRepository.Object, _mockCompanyTypeRepository.Object, _mockStateRepository.Object, _mockAddressRepository.Object, _mockCompanySegmentRepository.Object, _accessor);

        }

        [TestMethod]
        public void FindByNameParts_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<GetCompaniesPagedFilter>();
            _mockRepository.Setup(x => x.FindByNameParts(It.IsAny<GetCompaniesPagedFilter>(), It.IsAny<AccessControlDTO>()).Result).Returns(CompanySeed.GetPagedDto());

            // Act
            var result = _service.FindByNameParts(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.FindByNameParts(It.IsAny<GetCompaniesPagedFilter>(), It.IsAny<AccessControlDTO>()));
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
            _mockCompanyTypeRepository.Setup(r => r.GetAllTypes(It.IsAny<AccessControlDTO>()).Result).Returns(CompanyTypeSeed.GetCompanyTypes());

            // Act
            var result = _service.GetAllTypes();

            // Assert
            _mockCompanyTypeRepository.Verify(a => a.GetAllTypes(It.IsAny<AccessControlDTO>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsInstanceOfType(result.Result.Data, typeof(IList<CompanyTypeViewModel>));
        }

        [TestMethod]
        public void GetSegmetsByTypeId_MustReturnOk()
        {
            // Arrange
            var typeId = Guid.NewGuid();
            var filter = Mock.Of<PageFilter>();
            _mockCompanySegmentRepository.Setup(r => r.GetPagedByTypeId(typeId, filter, It.IsAny<AccessControlDTO>()).Result).Returns(CompanySegmentSeed.GetPaged());

            // Act
            var result = _service.GetSegmetsByTypeId(typeId, filter);

            // Assert
            _mockCompanySegmentRepository.Verify(a => a.GetPagedByTypeId(typeId, filter, It.IsAny<AccessControlDTO>()), Times.Once);
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
            _mockRepository.Setup(x => x.GetPrivateHeadquarters(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<AccessControlDTO>()).Result).Returns(CompanySeed.GetCompanies());

            // Act
            var result = _service.GetHeadquartersCompanies(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.GetPrivateHeadquarters(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<AccessControlDTO>()));
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
            _mockRepository.Setup(x => x.GetPublicMunicipalHeadquarters(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()).Result).Returns(CompanySeed.GetCompanies());
            _mockStateRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(StateSeed.GetState());

            // Act
            var result = _service.GetHeadquartersCompanies(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.GetPublicMunicipalHeadquarters(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()));
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

            _mockRepository.Setup(x => x.GetPublicStateHeadquarters(It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()).Result).Returns(CompanySeed.GetCompanies());

            // Act
            var result = _service.GetHeadquartersCompanies(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.GetPublicStateHeadquarters(It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()));
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
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.IsActive == null && x.Address == new AddressCreateDto() { CityId = Guid.NewGuid() });

            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["RequiredField"], _localizer["IsActive"]), ex.Message);
        }
        [TestMethod]
        public void Add_NotSaveDuplicateCnpj()
        {
            //Arrage
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.IsActive == true && x.Address == new AddressCreateDto() { CityId = Guid.NewGuid() });
            _mockRepository.Setup(x => x.CnpjAlreadyRegistered(It.IsAny<string>()).Result).Returns(true);
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(string.Format(string.Format(_localizer["DataAlreadyRegistered"], _localizer["Cnpj"])), ex.Message);
            _mockRepository.Verify(x => x.CnpjAlreadyRegistered(It.IsAny<string>()));
        }
        [TestMethod]
        public void Add_MustHaveValidAddress()
        {
            //Arrage
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.IsActive == false && x.Address == new AddressCreateDto() { CityId = Guid.Empty});

            _mockCityRepository.Setup(x => x.Find(Guid.Empty).Result);

            //Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(_localizer["CityNotFound"], ex.Message);
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
        public void Add_PrivateType_MustHaveIsHeadquartersValue()
        {
            // Arrange
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == null);
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(CompanySegmentSeed.GetHealthType());


            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["RequiredField"], _localizer["IsHeadquarters"]), ex.Message);

        }
        [TestMethod]
        public void Add_PrivateType_Headquarter_MustNotHaveParentIdValue()
        {
            // Arrange
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == true && x.ParentId == Guid.NewGuid());
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(CompanySegmentSeed.GetHealthType());

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["FieldMustBeNull"], _localizer["ParentId"]), ex.Message);

        }
        [TestMethod]
        public void Add_PrivateType_Headquarter_MustNotActiveHeadquartersWithSameCnpjCompanyIdentifyPart()
        {
            // Arrange
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == true && x.ParentId == null && x.Cnpj == "42192517000170");
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(CompanySegmentSeed.GetHealthType());
            _mockRepository.Setup(x => x.HasActiveHeadquartersWithSameCnpjCompanyIdentifyPart(It.IsAny<string>(),null).Result).Returns(true);
            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["AlreadyExistActiveHeadquartersWithSameCnpjCompanyIdentifyPart"], mockRequest.Cnpj.Substring(0,8)), ex.Message);
        }
        [TestMethod]
        public void Add_PrivateType_BranchCompany_MustHaveParentIdValue()
        {
            // Arrange
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == false && x.ParentId == null);
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(CompanySegmentSeed.GetHealthType());

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["RequiredField"], _localizer["ParentId"]), ex.Message);

        }
        [TestMethod]
        [DataRow(null,  null, null)]
        [DataRow(false,true, ECompanySegmentType.Health)]
        [DataRow(true,false, ECompanySegmentType.Health)]
        [DataRow(true,true, ECompanySegmentType.Municipal)]
        [DataRow(true,true, ECompanySegmentType.Contractor)]
        public void Add_PrivateType_BranchCompany_MustBeValidHeadquarter(bool? Isheadquarter,  bool? isActive, ECompanySegmentType? segmentType)
        {
            // Arrange
            var headquarterReturn = CompanySeed.Get(Isheadquarter, isActive, segmentType);
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == false && x.ParentId == Guid.NewGuid());
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(CompanySegmentSeed.GetHealthType());
            if (headquarterReturn != null)
                _mockRepository.Setup(x => x.GetLoadedCompanyById(It.IsAny<Guid>()).Result).Returns(headquarterReturn);

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(_localizer["HeadquarterNotFoundOrNotValid"], ex.Message);

        }

        [TestMethod]
        [DataRow("66379676000106")]
        [DataRow("45913289000178")]
        [DataRow("4591328")]
        public void Add_PrivateType_BranchCompany_MustBeValidBranchCnpj(string cnpj)
        {
            // Arrange
            var HeadquarterCpf = "66379676000146";
            var headquarterName = "EmpresaMatriz";
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == false && x.ParentId == Guid.NewGuid()
                    && x.Cnpj == cnpj);
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(CompanySegmentSeed.GetHealthType());            
            _mockRepository.Setup(x => x.GetLoadedCompanyById(It.IsAny<Guid>()).Result).Returns(CompanySeed.Get(true,true, ECompanySegmentType.Health,HeadquarterCpf,headquarterName));

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["BranchCnpjNotValid"], headquarterName), ex.Message);
        }
        [TestMethod]
        public void Add_GovernmentType_MustNotHaveIsHeadquartersValue()
        {
            // Arrange
            var governmentSegment = CompanySegmentSeed.GetMunicipalType();
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == false);
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(governmentSegment);

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["FieldMustBeNull"], _localizer["IsHeadquarters"]), ex.Message);
        }
        [TestMethod]
        public void Add_GovernmentType_FederalSegment_MustNotHaveParentIdValue()
        {
            // Arrange
            var federalSegment = CompanySegmentSeed.GetFederalType();
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == null && x.ParentId == Guid.NewGuid());
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(federalSegment);

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["FieldMustBeNull"], _localizer["ParentId"]), ex.Message);
        }
        [TestMethod]
        [DataRow(ECompanySegmentType.Federal, "Country")]
        [DataRow(ECompanySegmentType.State, "State")]
        [DataRow(ECompanySegmentType.Municipal, "City")]
        public void Add_GovernmentType_MustNotHaveSameLocaleCompany(ECompanySegmentType segmentType, string messagePart)
        {
            // Arrange
            var segment = CompanySegmentSeed.Get(segmentType);
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == null && x.ParentId == null);
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(segment);
            _mockCityRepository.Setup(x => x.FindLoadedById(It.IsAny<Guid>()).Result).Returns(CitySeed.GetLoaded());
            _mockRepository.Setup(x => x.HasSameSegmentAndLocaleGovernmentCompany(It.IsAny<Guid>(), segmentType, null).Result).Returns(true);

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["CompanyAlreadyRegisteredToSegmentAndLocal"], _localizer[messagePart]), ex.Message);
        }
        [TestMethod]
        [DataRow(ECompanySegmentType.State)]
        [DataRow(ECompanySegmentType.Municipal)]
        public void Add_GovernmentType_StateOrMunicipalSegments_WithoutCandidateParentCompany_MustNotHaveParentId(ECompanySegmentType segmentType)
        {
            // Arrange
            var segment = CompanySegmentSeed.Get(segmentType);
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == null && x.ParentId == Guid.NewGuid());

            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(segment);
            _mockCityRepository.Setup(x => x.FindLoadedById(It.IsAny<Guid>()).Result).Returns(CitySeed.GetLoaded());
            _mockRepository.Setup(x => x.HasSameSegmentAndLocaleGovernmentCompany(It.IsAny<Guid>(), segmentType, null).Result).Returns(false);
            if (ECompanySegmentType.Municipal == segmentType)
                _mockRepository.Setup(x => x.GetPublicMunicipalHeadquarters(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()).Result).Returns(Mock.Of<List<Company>>());
            else
                _mockRepository.Setup(x => x.GetPublicStateHeadquarters(It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()).Result).Returns(Mock.Of<List<Company>>());

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["FieldMustBeNull"], _localizer["ParentId"]), ex.Message);
        }
        [TestMethod]
        [DataRow(ECompanySegmentType.State)]
        [DataRow(ECompanySegmentType.Municipal)]
        public void Add_GovernmentType_StateOrMunicipalSegments_WithoutCandidateParentCompanyActive_MustNotHaveParentId(ECompanySegmentType segmentType)
        {
            // Arrange
            var segment = CompanySegmentSeed.Get(segmentType);
            var candidateParentCompany = CompanySeed.GetCompanies(false);
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == null && x.ParentId == Guid.NewGuid());

            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(segment);
            _mockCityRepository.Setup(x => x.FindLoadedById(It.IsAny<Guid>()).Result).Returns(CitySeed.GetLoaded());
            _mockRepository.Setup(x => x.HasSameSegmentAndLocaleGovernmentCompany(It.IsAny<Guid>(), segmentType, null).Result).Returns(false);
            if (ECompanySegmentType.Municipal == segmentType)
                _mockRepository.Setup(x => x.GetPublicMunicipalHeadquarters(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()).Result).Returns(candidateParentCompany);
            else
                _mockRepository.Setup(x => x.GetPublicStateHeadquarters(It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()).Result).Returns(candidateParentCompany);

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["FieldMustBeNull"], _localizer["ParentId"]), ex.Message);
        }
        [TestMethod]
        [DataRow(ECompanySegmentType.State)]
        [DataRow(ECompanySegmentType.Municipal)]
        public void Add_GovernmentType_StateOrMunicipalSegments_MustHaveParentId_In_CandidateParentCompanies(ECompanySegmentType segmentType)
        {
            // Arrange
            var segment = CompanySegmentSeed.Get(segmentType);
            var candidateParentCompany = CompanySeed.GetCompanies(true);
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == null && x.ParentId == Guid.Empty);

            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(segment);
            _mockCityRepository.Setup(x => x.FindLoadedById(It.IsAny<Guid>()).Result).Returns(CitySeed.GetLoaded());
            _mockRepository.Setup(x => x.HasSameSegmentAndLocaleGovernmentCompany(It.IsAny<Guid>(), segmentType, null).Result).Returns(false);
            if (ECompanySegmentType.Municipal == segmentType)
                _mockRepository.Setup(x => x.GetPublicMunicipalHeadquarters(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()).Result).Returns(candidateParentCompany);
            else
                _mockRepository.Setup(x => x.GetPublicStateHeadquarters(It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()).Result).Returns(candidateParentCompany);

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(_localizer["HeadquarterNotFoundOrNotValid"], ex.Message);
        }
        [TestMethod]
        [DataRow(ECompanySegmentType.Contractor, true)]
        [DataRow(ECompanySegmentType.Contractor, false)]
        public void Add_WhenNotSave_MustReturnError(ECompanySegmentType segmentType, bool IsActive)
        {
            // Arrange
            var segment = CompanySegmentSeed.Get(segmentType);
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == IsActive && x.IsHeadquarters == true && x.ParentId == null);

            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(false);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(segment);

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Add(mockRequest)).Result;
            Assert.AreEqual(_localizer["OperationNotPerformed"], ex.Message);
        }

        [TestMethod]
        [DataRow(ECompanySegmentType.Federal, null)]
        [DataRow(ECompanySegmentType.State, null)]
        [DataRow(ECompanySegmentType.Municipal, null)]
        [DataRow(ECompanySegmentType.Health, true)]
        [DataRow(ECompanySegmentType.Contractor, true)]
        public void Add_MustReturnOk(ECompanySegmentType segmentType, bool? isHeadquarters)
        {
            // Arrange
            var segment = CompanySegmentSeed.Get(segmentType);
            var mockRequest = Mock.Of<CompanyCreateDto>(x => x.Address == Mock.Of<AddressCreateDto>()
                    && x.IsActive == true && x.IsHeadquarters == isHeadquarters && x.ParentId == null);

            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(segment);
            _mockRepository.Setup(x => x.GetLoadedCompanyById(It.IsAny<Guid>()).Result)
                    .Returns(CompanySeed.Get(isHeadquarters, true, segmentType));
            _mockRepository.Setup(x => x.HasSameSegmentAndLocaleGovernmentCompany(It.IsAny<Guid>(), segmentType, null).Result).Returns(false);
            _mockCityRepository.Setup(x => x.FindLoadedById(It.IsAny<Guid>()).Result).Returns(CitySeed.GetLoaded());
            
            if(ECompanySegmentType.Federal == segmentType)
                _mockRepository.Setup(x => x.HasSubsidiariesCandidatesToFederalGovernment(It.IsAny<Guid>()).Result).Returns(true);
            else if (ECompanySegmentType.Municipal == segmentType)
                _mockRepository.Setup(x => x.GetPublicMunicipalHeadquarters(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()).Result).Returns(Mock.Of<List<Company>>());
            else if (ECompanySegmentType.State == segmentType)
            {
                _mockRepository.Setup(x => x.GetPublicStateHeadquarters(It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()).Result).Returns(Mock.Of<List<Company>>());
                _mockRepository.Setup(x => x.HasSubsidiariesCandidatesToStateGovernment(It.IsAny<Guid>()).Result).Returns(false);
            }

            //Act
            var result = _service.Add(mockRequest);

            // Assert            
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.AreEqual(true, result.Result.Success);
            Assert.IsInstanceOfType(result.Result.Data, typeof(CompanyCreateResponseViewModel));

        }

        #endregion

        [TestMethod]
        [DataRow(ECompanySegmentType.Federal, null)]
        [DataRow(ECompanySegmentType.State, null)]
        [DataRow(ECompanySegmentType.Municipal, null)]
        [DataRow(ECompanySegmentType.Health, true)]
        [DataRow(ECompanySegmentType.Contractor, true)]
        public void Edit_MustReturnOk(ECompanySegmentType segmentType, bool? isHeadquarters)
        {
            // Arrange
            var segment = CompanySegmentSeed.Get(segmentType);
            var mockRequest = Mock.Of<CompanyEditDto>(x => x.Id == Guid.NewGuid()
                    && x.Responsible == Mock.Of<CompanyResponsibleDto>(y => y.CompanyId == x.Id)
                    && x.Address == Mock.Of<AddressEditDto>(z => z.Id == Guid.NewGuid()) 
                    && x.IsActive == true 
                    && x.IsHeadquarters == isHeadquarters 
                    && x.ParentId == null);

            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.Update(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(segment);
            _mockRepository.Setup(x => x.GetLoadedCompanyById(It.IsAny<Guid>()).Result)
                    .Returns(CompanySeed.Get(isHeadquarters, true, segmentType));
            _mockRepository.Setup(x => x.HasSameSegmentAndLocaleGovernmentCompany(It.IsAny<Guid>(), segmentType, It.IsAny<Guid>()).Result).Returns(false);
            _mockCityRepository.Setup(x => x.FindLoadedById(It.IsAny<Guid>()).Result).Returns(CitySeed.GetLoaded());

            if (ECompanySegmentType.Federal == segmentType)
                _mockRepository.Setup(x => x.HasSubsidiariesCandidatesToFederalGovernment(It.IsAny<Guid>()).Result).Returns(true);
            else if (ECompanySegmentType.Municipal == segmentType)
                _mockRepository.Setup(x => x.GetPublicMunicipalHeadquarters(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()).Result).Returns(Mock.Of<List<Company>>());
            else if (ECompanySegmentType.State == segmentType)
            {
                _mockRepository.Setup(x => x.GetPublicStateHeadquarters(It.IsAny<Guid>(), It.IsAny<AccessControlDTO>()).Result).Returns(Mock.Of<List<Company>>());
                _mockRepository.Setup(x => x.HasSubsidiariesCandidatesToStateGovernment(It.IsAny<Guid>()).Result).Returns(false);
            }

            //Act
            var result = _service.Edit(mockRequest);

            // Assert
            _mockRepository.Verify(x => x.Update(It.IsAny<Company>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.AreEqual(true, result.Result.Success);
        }
        [TestMethod]
        [DataRow(ECompanySegmentType.Federal, "Country")]
        [DataRow(ECompanySegmentType.State, "State")]
        [DataRow(ECompanySegmentType.Municipal, "City")]
        public void Edit_GovernmentType_MustNotHaveSameLocaleCompany(ECompanySegmentType segmentType, string messagePart)
        {
            // Arrange
            var segment = CompanySegmentSeed.Get(segmentType);
            var mockRequest = Mock.Of<CompanyEditDto>(x => x.Address == Mock.Of<AddressEditDto>() && x.Id == Guid.NewGuid()
                    && x.IsActive == true && x.IsHeadquarters == null && x.ParentId == null);
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(segment);
            _mockCityRepository.Setup(x => x.FindLoadedById(It.IsAny<Guid>()).Result).Returns(CitySeed.GetLoaded());
            _mockRepository.Setup(x => x.HasSameSegmentAndLocaleGovernmentCompany(It.IsAny<Guid>(), segmentType, It.IsAny<Guid>()).Result).Returns(true);
            _mockRepository.Setup(x => x.GetLoadedCompanyById(It.IsAny<Guid>()).Result).Returns(CompanySeed.Get());

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Edit(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["CompanyAlreadyRegisteredToSegmentAndLocal"], _localizer[messagePart]), ex.Message);
        }
        [TestMethod]
        public void Edit_PrivateType_Headquarter_MustNotActiveHeadquartersWithSameCnpjCompanyIdentifyPart()
        {
            // Arrange
            var mockRequest = Mock.Of<CompanyEditDto>(x => x.Address == Mock.Of<AddressEditDto>() && x.Id == Guid.NewGuid()
                    && x.IsActive == true && x.IsHeadquarters == true && x.ParentId == null && x.Cnpj == "42192517000170");
            _mockCityRepository.Setup(x => x.Find(It.IsAny<Guid>()).Result).Returns(CitySeed.Get());
            _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Company>()).Result).Returns(true);
            _mockCompanySegmentRepository.Setup(x => x.GetLoaded(It.IsAny<Guid>()).Result).Returns(CompanySegmentSeed.GetHealthType());
            _mockRepository.Setup(x => x.GetLoadedCompanyById(It.IsAny<Guid>()).Result).Returns(CompanySeed.Get());
            _mockRepository.Setup(x => x.HasActiveHeadquartersWithSameCnpjCompanyIdentifyPart(It.IsAny<string>(), It.IsAny<Guid>()).Result).Returns(true);
            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.Edit(mockRequest)).Result;
            Assert.AreEqual(string.Format(_localizer["AlreadyExistActiveHeadquartersWithSameCnpjCompanyIdentifyPart"], mockRequest.Cnpj.Substring(0, 8)), ex.Message);
        }

        [TestMethod]
        [DataRow(null, null, null)]
        [DataRow(false, ECompanySegmentType.State, true)]
        [DataRow(false, ECompanySegmentType.Federal, true)]
        [DataRow(true, ECompanySegmentType.State, false)]
        [DataRow(true, ECompanySegmentType.Federal, false)]
        [DataRow(true, null, true)]
        [DataRow(true, ECompanySegmentType.Municipal, true)]
        [DataRow(true, ECompanySegmentType.Contractor, true)]
        [DataRow(true, ECompanySegmentType.Health, true)]
        public void GetSubsidiariesCandidatesPaged_MustBeValidParent(bool? isActive, ECompanySegmentType? segment, bool? hasAddress )
        {
            // Arrange
            var mockFilter = Mock.Of<PageFilter>();
            var parentCompany = CompanySeed.Get(null, isActive, segment, default, default, hasAddress);
            
            if(parentCompany != null)
                _mockRepository.Setup(x => x.GetLoadedCompanyById(It.IsAny<Guid>()).Result).Returns(parentCompany);
            

            // Assert
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await _service.GetSubsidiariesCandidatesPaged(Guid.NewGuid(), mockFilter)).Result;
            Assert.AreEqual(_localizer["HeadquarterNotFoundOrNotValid"], ex.Message);
        }

        [TestMethod]
        [DataRow(true, ECompanySegmentType.State)]
        [DataRow(true, ECompanySegmentType.Federal)]
        public void GetSubsidiariesCandidatesPaged_MustReturnOK(bool? isActive, ECompanySegmentType? segment)
        {
            // Arrange
            var mockFilter = Mock.Of<PageFilter>();
            var parentCompany = CompanySeed.Get(null, isActive, segment);
            _mockRepository.Setup(x => x.GetLoadedCompanyById(It.IsAny<Guid>()).Result).Returns(parentCompany);
            _mockRepository.Setup(x => x.GetSubsidiariesCandidatesToFederalGovernmentPaged(It.IsAny<Guid>(),mockFilter).Result).Returns(CompanySeed.GetPaged());
            _mockRepository.Setup(x => x.GetSubsidiariesCandidatesToStateGovernmentPaged(It.IsAny<Guid>(), mockFilter).Result).Returns(CompanySeed.GetPaged());

            //Act
            var result = _service.GetSubsidiariesCandidatesPaged(Guid.NewGuid(), mockFilter);


            // Assert
            _mockRepository.Verify(x => x.GetLoadedCompanyById(It.IsAny<Guid>()));
            if (segment == ECompanySegmentType.State)
                _mockRepository.Verify(x => x.GetSubsidiariesCandidatesToStateGovernmentPaged(It.IsAny<Guid>(), mockFilter));
            else
                _mockRepository.Verify(x => x.GetSubsidiariesCandidatesToFederalGovernmentPaged(It.IsAny<Guid>(), mockFilter));
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.AreEqual(true, result.Result.Success);
            Assert.AreEqual(_localizer["SubsidiariesCandidatesCompanies"], result.Result.Message);
            Assert.IsInstanceOfType(result.Result.Data, typeof(CompanySubsidiaryCandidateResponseViewModel));
        }

        [TestMethod]
        [DataRow(false, true, ECompanySegmentType.State)]
        [DataRow(true, true, ECompanySegmentType.State)]
        [DataRow(false, true, ECompanySegmentType.Federal)]
        [DataRow(true, true, ECompanySegmentType.Federal)]
        public void AssociateSubsidiaries(bool associateAll, bool isActive, ECompanySegmentType? segment)
        {
            //Arrange
            List<Guid> mockSubs = null;
            if (!associateAll)
                mockSubs = new List<Guid>()
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                };

            var mockRequest = Mock.Of<AssociateSubsidiariesDto>(x => x.AssociateAll == associateAll && x.ParentId == Guid.NewGuid() && x.Subsidiaries == mockSubs);
            var parentCompany = CompanySeed.Get(null, isActive, segment);
            
            _mockRepository.Setup(x => x.GetLoadedCompanyById(It.IsAny<Guid>()).Result).Returns(parentCompany);
            _mockRepository.Setup(x => x.GetSubsidiariesCandidatesToFederalGovernment(It.IsAny<Guid>(), It.IsAny<List<Guid>>()).Result).Returns(CompanySeed.GetCompanies());
            _mockRepository.Setup(x => x.GetSubsidiariesCandidatesToStateGovernment(It.IsAny<Guid>(), It.IsAny<List<Guid>>()).Result).Returns(CompanySeed.GetCompanies());
            _mockRepository.Setup(x => x.Update(It.IsAny<Company>()).Result).Returns(true);
            
            //Act
            var result = _service.AssociateSubsidiaries(mockRequest);

            //Assert
            if (segment == ECompanySegmentType.State)
                _mockRepository.Verify(x => x.GetSubsidiariesCandidatesToStateGovernment(It.IsAny<Guid>(), It.IsAny<List<Guid>>()));
            else
                _mockRepository.Verify(x => x.GetSubsidiariesCandidatesToFederalGovernment(It.IsAny<Guid>(), It.IsAny<List<Guid>>()));

            _mockRepository.Verify(x => x.Update(It.IsAny<Company>()));
            _mockRepository.Verify(x => x.GetLoadedCompanyById(It.IsAny<Guid>()));
            
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.AreEqual(true, result.Result.Success);
        }
    }
}

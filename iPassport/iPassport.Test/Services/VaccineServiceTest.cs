using AutoMapper;
using iPassport.Api.Models.Requests.Vaccine;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class VaccineServiceTest
    {
        Mock<IUserVaccineRepository> _userVaccineRepository;
        Mock<IVaccineDosageTypeRepository> _vaccineDosageTypeRepository;
        Mock<IVaccinePeriodTypeRepository> _vaccinePeriodTypeRepository;
        Mock<IVaccineRepository> _vaccineRepository;
        IVaccineService _service;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;
        IMapper _mapper;
        IHttpContextAccessor _accessor;
        Mock<IUnitOfWork> _mockUnitOfWork;
        Mock<IDiseaseRepository> _diseaseRepository;

        [TestInitialize]
        public void Setup()
        {
            _userVaccineRepository = new Mock<IUserVaccineRepository>();
            _vaccineDosageTypeRepository = new Mock<IVaccineDosageTypeRepository>();
            _vaccinePeriodTypeRepository = new Mock<IVaccinePeriodTypeRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
            _vaccineRepository = new Mock<IVaccineRepository>();
            _mapper = AutoMapperFactory.Create();
            _accessor = HttpContextAccessorFactory.Create();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _diseaseRepository = new Mock<IDiseaseRepository>();

            _service = new VaccineService(_vaccineRepository.Object, _userVaccineRepository.Object, _vaccinePeriodTypeRepository.Object, _vaccineDosageTypeRepository.Object, _diseaseRepository.Object, _mockUnitOfWork.Object, _accessor, _mockLocalizer.Object, _mapper);
        }

        [TestMethod]
        public void GetByNameInitals()
        {
            var mockFilter = Mock.Of<GetVaccinatedCountFilter>();

            // Arrange
            _userVaccineRepository.Setup(r => r.GetVaccinatedCount(It.IsAny<GetVaccinatedCountFilter>()).Result).Returns(3);

            // Act
            var result = _service.GetVaccinatedCount(mockFilter);

            // Assert
            _userVaccineRepository.Verify(a => a.GetVaccinatedCount(It.IsAny<GetVaccinatedCountFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetByManufacturerId()
        {
            var mockFilter = Mock.Of<GetVaccineByManufacturerFilter>();

            var vaccines = VaccineSeed.GetVaccines();
            vaccines.ToList().ForEach(x =>
            {
                x.AgeGroupVaccines = new List<AgeGroupVaccine>();
                x.GeneralGroupVaccine = new GeneralGroupVaccine();
            });

            // Arrange
            _vaccineRepository.Setup(r => r.GetPagged(It.IsAny<GetPagedVaccinesFilter>()).Result).Returns(new PagedData<Vaccine>() { Data = vaccines });

            // Act
            var result = _service.GetByManufacturerId(mockFilter);

            // Assert
            _vaccineRepository.Verify(a => a.GetPagged(It.IsAny<GetPagedVaccinesFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetPagged()
        {
            var mockFilter = Mock.Of<GetPagedVaccinesFilter>();

            // Arrange
            _vaccineRepository.Setup(r => r.GetPagged(It.IsAny<GetPagedVaccinesFilter>()).Result).Returns(new PagedData<Vaccine>() { Data = VaccineSeed.GetVaccines() });

            // Act
            var result = _service.GetPagged(mockFilter);

            // Assert
            _vaccineRepository.Verify(a => a.GetPagged(It.IsAny<GetPagedVaccinesFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void Add()
        {
            var mockRequest = Mock.Of<VaccineDto>(x => x.DosageType == EVaccineDosageType.GeneralGroup && x.GeneralGroupVaccine == new GeneralGroupVaccineDto() { PeriodType = EVaccinePeriodType.Variable, RequiredDoses = 2, TimeNextDoseMax = 2, TimeNextDoseMin = 1 });

            //Arranje
            _vaccinePeriodTypeRepository.Setup(x => x.GetByIdentifyer(It.IsAny<int>()).Result).Returns(Mock.Of<VaccinePeriodType>());
            _vaccineDosageTypeRepository.Setup(x => x.GetByIdentifyer(It.IsAny<int>()).Result).Returns(Mock.Of<VaccineDosageType>());
            _diseaseRepository.Setup(x => x.GetByIdList(It.IsAny<IList<Guid>>()).Result).Returns(Mock.Of<IList<Disease>>());
            _vaccineRepository.Setup(x => x.InsertAsync(It.IsAny<Vaccine>()).Result).Returns(true);
            _vaccineRepository.Setup(x => x.AssociateDiseases(It.IsAny<Vaccine>(), It.IsAny<IList<Disease>>()).Result).Returns(true);

            // Act
            var result = _service.Add(mockRequest);

            // Assert
            _vaccineRepository.Verify(a => a.InsertAsync(It.IsAny<Vaccine>()), Times.Once);
            _vaccineRepository.Verify(a => a.AssociateDiseases(It.IsAny<Vaccine>(), It.IsAny<IList<Disease>>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);

        }
    }
}

using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

        [TestInitialize]
        public void Setup()
        {
            _userVaccineRepository = new Mock<IUserVaccineRepository>();
            _vaccineDosageTypeRepository = new Mock<IVaccineDosageTypeRepository>();
            _vaccinePeriodTypeRepository = new Mock<IVaccinePeriodTypeRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
            _vaccineRepository = new Mock<IVaccineRepository>();
            _mapper = AutoMapperFactory.Create();

            _service = new VaccineService(_vaccineRepository.Object, _userVaccineRepository.Object, _vaccinePeriodTypeRepository.Object, _vaccineDosageTypeRepository.Object, _mockLocalizer.Object, _mapper);
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
    }
}

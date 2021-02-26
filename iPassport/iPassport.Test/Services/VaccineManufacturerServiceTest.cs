using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Services;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class VaccineManufacturerServiceTest
    {
        Mock<IVaccineManufacturerRepository> _mockRepository;
        IVaccineManufacturerService _service;
        IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IVaccineManufacturerRepository>();
            _service = new VaccineManufacturerService(_mapper, _mockRepository.Object);
        }

        [TestMethod]
        public void GetByNameInitals()
        {
            var mockFilter = Mock.Of<GetByNameInitalsFilter>();

            // Arrange
            _mockRepository.Setup(r => r.GetByNameInitals(It.IsAny<GetByNameInitalsFilter>()).Result).Returns(new PagedData<VaccineManufacturer>());

            // Act
            var result = _service.GetByNameInitals(mockFilter);

            // Assert
            _mockRepository.Verify(a => a.GetByNameInitals(It.IsAny<GetByNameInitalsFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }
    }
}

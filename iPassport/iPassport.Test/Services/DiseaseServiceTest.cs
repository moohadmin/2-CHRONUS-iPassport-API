using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Test.Settings.Factories;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class DiseaseServiceTest
    {
        Mock<IDiseaseRepository> _mockRepository;
        IDiseaseService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
            _mockRepository = new Mock<IDiseaseRepository>();
            _service = new DiseaseService(_mapper, _mockRepository.Object, _mockLocalizer.Object);
        }

        [TestMethod]
        public void GetByNameInitals()
        {
            var mockFilter = Mock.Of<GetByNameInitialsPagedFilter>();
            
            // Arrange
            _mockRepository.Setup(r => r.GetByNameInitals(It.IsAny<GetByNameInitialsPagedFilter>()).Result).Returns(new PagedData<Disease>());

            // Act
            var result = _service.GetByNameInitals(mockFilter);

            // Assert
            _mockRepository.Verify(a => a.GetByNameInitals(It.IsAny<GetByNameInitialsPagedFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }
    }
}

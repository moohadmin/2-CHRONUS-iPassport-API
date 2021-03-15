using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Test.Settings.Factories;
using iPassport.Test.Settings.Seeds;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class UserDiseaseTestServiceTest
    {
        Mock<IUserDiseaseTestRepository> _mockRepository;
        IUserDiseaseTestService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IUserDiseaseTestRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();

            var accessor = HttpContextAccessorFactory.Create();

            _service = new UserDiseaseTestService(_mapper, _mockRepository.Object, _mockLocalizer.Object, accessor);
        }

        [TestMethod]
        public void GetPagedUserVaccines()
        {
            var seed = UserDiseaseTestSeed.GetUserDiseaseTests();

            var mockFilter = Mock.Of<GetByIdPagedFilter>();

            // Arrange
            _mockRepository.Setup(r => r.GetPaggedUserDiseaseByPassportId(It.IsAny<GetByIdPagedFilter>()).Result).Returns(seed);

            // Act
            var result = _service.GetUserDiseaseTest(mockFilter);

            // Assert
            _mockRepository.Verify(a => a.GetPaggedUserDiseaseByPassportId(It.IsAny<GetByIdPagedFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetCurrentUserVaccines()
        {
            var seed = UserDiseaseTestSeed.GetUserDiseaseTests();

            var mockFilter = Mock.Of<PageFilter>();

            // Arrange
            _mockRepository.Setup(r => r.GetPagedUserDiseaseByUserId(It.IsAny<GetByIdPagedFilter>()).Result).Returns(seed);

            // Act
            var result = _service.GetCurrentUserDiseaseTest(mockFilter);

            // Assert
            _mockRepository.Verify(a => a.GetPagedUserDiseaseByUserId(It.IsAny<GetByIdPagedFilter>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }
    }
}

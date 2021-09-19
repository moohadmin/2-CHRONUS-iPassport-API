using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Dtos;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Test.Settings.Factories;
using iPassport.Test.Settings.Seeds;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class ProfileServiceTest
    {
        Mock<IProfileRepository> _mockRepository;
        IProfileService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;
        IHttpContextAccessor _accessor;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IProfileRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
            _accessor = HttpContextAccessorFactory.Create();

            _service = new ProfileService(_mockRepository.Object, _mapper, _mockLocalizer.Object, _accessor);
        }

        [TestMethod]
        public void GetAll()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll(It.IsAny<AccessControlDTO>()).Result).Returns(ProfileSeed.GetProfiles());

            // Act
            var result = _service.GetAll();

            // Assert
            _mockRepository.Verify(a => a.GetAll(It.IsAny<AccessControlDTO>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.AreEqual(true, result.Result.Success);
        }
    }
}

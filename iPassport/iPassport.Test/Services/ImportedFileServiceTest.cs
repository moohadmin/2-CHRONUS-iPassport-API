using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class ImportedFileServiceTest
    {
        Mock<IImportedFileRepository> _mockRepository;
        Mock<IImportedFileDetailsRepository> _mockFileDetailsRepository;
        Mock<IUserRepository> _mockuserRepository;
        IImportedFileService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<IImportedFileRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
            _mockFileDetailsRepository = new Mock<IImportedFileDetailsRepository>();
            _mockuserRepository = new Mock<IUserRepository>();
            _service = new ImportedFileService(_mapper, _mockRepository.Object, _mockLocalizer.Object
                , _mockuserRepository.Object, _mockFileDetailsRepository.Object);
        }

        [TestMethod]
        public void FindByPeriod_MustReturnOk()
        {
            // Arrange
            var mockRequest = Mock.Of<GetImportedFileFilter>();
            _mockRepository.Setup(x => x.FindByPeriod(It.IsAny<GetImportedFileFilter>()).Result).Returns(ImportedFileSeed.GetPaged());
            _mockuserRepository.Setup(x => x.GetById(It.IsAny<Guid>()).Result).Returns(UserSeed.GetUser());
            // Act
            var result = _service.FindByPeriod(mockRequest);

            // Assert
            _mockRepository.Verify(x => x.FindByPeriod(It.IsAny<GetImportedFileFilter>()));
            _mockuserRepository.Verify(x => x.GetById(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<PagedResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetImportedFileDetails_MustReturnOk()
        {
            // Arrange
            _mockFileDetailsRepository.Setup(x => x.GetByFileId(It.IsAny<Guid>()).Result)
                .Returns(ImportedFileSeed.GetImportedFileDetails());
            // Act
            var result = _service.GetImportedFileDetails(It.IsAny<Guid>());

            // Assert
            _mockFileDetailsRepository.Verify(x => x.GetByFileId(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }
    }
}

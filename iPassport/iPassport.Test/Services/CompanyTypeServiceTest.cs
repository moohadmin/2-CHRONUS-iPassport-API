using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Application.Services;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Test.Settings.Factories;
using iPassport.Test.Settings.Seeds;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class CompanyTypeServiceTest
    {
        Mock<ICompanyTypeRepository> _mockRepository;
        ICompanyTypeService _service;
        IMapper _mapper;
        Mock<IStringLocalizer<Resource>> _mockLocalizer;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockRepository = new Mock<ICompanyTypeRepository>();
            _mockLocalizer = new Mock<IStringLocalizer<Resource>>();
            _service = new CompanyTypeService(_mapper, _mockRepository.Object, _mockLocalizer.Object);
        }

        [TestMethod]
        public void GetAll_MustReturnOk()
        {
            // Arrange
            _mockRepository.Setup(r => r.FindAll().Result).Returns(CompanyTypeSeed.GetCompanyTypes());

            // Act
            var result = _service.GetAll();

            // Assert
            _mockRepository.Verify(a => a.FindAll(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsInstanceOfType(result.Result.Data, typeof(IList<CompanyTypeViewModel>));
        }
    }
}

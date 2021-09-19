using iPassport.Api.Models.Requests.Company;
using iPassport.Api.Models.Validators.Company;
using iPassport.Application.Resources;
using iPassport.Test.Settings.Factories;
using iPassport.Test.Settings.Seeds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace iPassport.Test.Controllers.Validator
{

    [TestClass]
    public class GetHeadquarterCompanyRequestValidatorTest
    {
        GetHeadquarterCompanyRequestValidator _validator;
        public Resource resource { get; private set; }

        [TestInitialize]
        public void Setup()
        {
            resource = ResourceFactory.Create();
            _validator = new(ResourceFactory.GetStringLocalizer());
        }

        [TestMethod]
        public void Success()
        {
            Assert.IsTrue(_validator.Validate(
                new GetHeadquarterCompanyRequest()
                {
                    Cnpj = "00000000",
                    CompanyTypeId = Guid.NewGuid(),
                    LocalityId = Guid.NewGuid(),
                    SegmentId = Guid.NewGuid()
                }).IsValid);
        }

        [TestMethod]
        public void CompanyTypeIdentifyerNullRequired()
        {
            // Act
            var validationResult = _validator.Validate(GetHeadquarterCompanyRequestValidatorSeed.GetHeadquarterCompanyRequestCompanyTypeIdentifyerNull());

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("CompanyType")), validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void SegmentTypeNullRequired()
        {
            // Act
            var validationResult = _validator.Validate(GetHeadquarterCompanyRequestValidatorSeed.GetHeadquarterCompanyRequestSegmentIdentifyerNull());

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("CompanySegmentType")), validationResult.Errors.Single().ErrorMessage);
        }
    }
}

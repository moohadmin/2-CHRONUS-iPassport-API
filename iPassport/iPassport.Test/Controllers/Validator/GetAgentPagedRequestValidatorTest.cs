using iPassport.Api.Models.Requests.Company;
using iPassport.Api.Models.Validators.Company;
using iPassport.Application.Resources;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace iPassport.Test.Controllers.Validator
{
    [TestClass]
    public class GetCompaniesPagedRequestValidatorTest
    {
        GetCompaniesPagedRequestValidator _validator;
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
            Assert.IsTrue(_validator.Validate(new GetCompaniesPagedRequest() { PageNumber = 1, PageSize = 10, Initials = "123" }).IsValid);
        }

        [TestMethod]
        public void InvalidInitals()
        {
            // Act
            var validationResult = _validator.Validate(new GetCompaniesPagedRequest() { PageNumber = 1, PageSize = 10, Initials = "12" });

            var expetedMessage = string.Format(resource.GetMessage("InitalsRequestMin"), "3");

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }
    }
}

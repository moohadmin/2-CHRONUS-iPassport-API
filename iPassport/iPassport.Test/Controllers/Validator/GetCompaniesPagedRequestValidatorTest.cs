using iPassport.Api.Models.Requests.Company;
using iPassport.Api.Models.Requests.User;
using iPassport.Api.Models.Validators.Users;
using iPassport.Application.Resources;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace iPassport.Test.Controllers.Validator
{
    [TestClass]
    public class GetAgentPagedRequestValidatorTest
    {
        GetAgentPagedRequestValidator _validator;
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
            Assert.IsTrue(_validator.Validate(new GetAgentPagedRequest() { PageNumber = 1, PageSize = 10, Initials = "123" }).IsValid);
        }

        [TestMethod]
        public void InvalidInitals()
        {
            // Act
            var validationResult = _validator.Validate(new GetAgentPagedRequest() { PageNumber = 1, PageSize = 10, Initials = "12" });

            var expetedMessage = string.Format(resource.GetMessage("InitalsRequestMin"), "3");

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void InvalidCpf()
        {
            // Act
            var validationResult = _validator.Validate(new GetAgentPagedRequest() { PageNumber = 1, PageSize = 10, Cpf = "00000000000" });

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), "CPF");

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }
    }
}

using iPassport.Api.Models.Requests.User;
using iPassport.Api.Models.Validators.Users;
using iPassport.Application.Resources;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace iPassport.Test.Controllers.Validator
{
    [TestClass]
    public class AdminCreateRequestValidatorTest
    {
        AdminCreateRequestValidator _validator;
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
                new AdminCreateRequest()
                {
                    CompanyId = Guid.NewGuid(),
                    CompleteName = "test test",
                    Cpf = "78282500020",
                    Email = "test@test.com",
                    HealthUnitId = Guid.NewGuid(),
                    IsActive = true,
                    Occupation = "test",
                    Password = "Test!123",
                    ProfileId = Guid.NewGuid(),
                    Telephone = "5571999999999"
                }).IsValid);
        }

        [TestMethod]
        [DataRow("123456789")]
        [DataRow("123456a789")]
        [DataRow("!@#$%¨%$#@")]
        public void InvalidCpf(string cpf)
        {
            var seed = new AdminCreateRequest()
            {
                CompanyId = Guid.NewGuid(),
                CompleteName = "test test",
                Cpf = cpf,
                Email = "test@test.com",
                HealthUnitId = Guid.NewGuid(),
                IsActive = true,
                Occupation = "test",
                Password = "Test!123",
                ProfileId = Guid.NewGuid(),
                Telephone = "5571999999999"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), "CPF");

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("123456789")]
        [DataRow("AAAABBBBBCCCCCCCC#!!*($")]
        [DataRow("!#$%¨%$#")]
        [DataRow("INVALID TEST")]
        public void InvalidEmail(string email)
        {
            var seed = new AdminCreateRequest()
            {
                CompanyId = Guid.NewGuid(),
                CompleteName = "test test",
                Cpf = "78282500020",
                Email = email,
                HealthUnitId = Guid.NewGuid(),
                IsActive = true,
                Occupation = "test",
                Password = "Test!123",
                ProfileId = Guid.NewGuid(),
                Telephone = "5571999989999"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("Email"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }
    }
}

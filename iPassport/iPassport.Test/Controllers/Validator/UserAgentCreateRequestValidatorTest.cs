using iPassport.Api.Models.Requests;
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
    public class UserAgentCreateRequestValidatorTest
    {
        UserAgentCreateRequestValidator _validator;
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
            var validator = _validator.Validate(
                new UserAgentCreateRequest()
                {
                    CompanyId = Guid.NewGuid(),
                    CompleteName = "test test",
                    Cpf = "78282500020",
                    Email = "test@test.com",
                    IsActive = true,
                    Password = "Test!123",
                    CellphoneNumber = "5571999999999",
                    CorporateCellphoneNumber = "5571999999999",
                    Address = new AddressCreateRequest()
                    {
                        Cep = "41700000",
                        CityId = Guid.NewGuid(),
                        Description = "test",
                        District = "test",
                        Number = "1"
                    }
                });
            Assert.IsTrue(validator.IsValid);
        }

        [TestMethod]
        [DataRow("123456789")]
        [DataRow("AAAABBBBBCCCCCCCC#!!*($")]
        [DataRow("!#$%¨%$#")]
        [DataRow("INVALID TEST")]
        public void InvalidEmail(string email)
        {
            var seed = new UserAgentCreateRequest()
                {
                    CompanyId = Guid.NewGuid(),
                    CompleteName = "test test",
                    Cpf = "78282500020",
                    Email = email,
                    IsActive = true,
                    Password = "Test!123",
                    CellphoneNumber = "5571999999999",
                    CorporateCellphoneNumber = "5571999999999",
                    Address = new AddressCreateRequest()
                    {
                        Cep = "41700000",
                        CityId = Guid.NewGuid(),
                        Description = "test",
                        District = "test",
                        Number = "1"
                    }
                };
            
            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("Email"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("123456789")]
        [DataRow("123456a789")]
        [DataRow("!@#$%¨%$#@")]
        public void InvalidCpf(string cpf)
        {
            var seed = new UserAgentCreateRequest()
            {
                CompanyId = Guid.NewGuid(),
                CompleteName = "test test",
                Cpf = cpf,
                Email = "test@test.com",
                IsActive = true,
                Password = "Test!123",
                CellphoneNumber = "5571999999999",
                CorporateCellphoneNumber = "5571999999999",
                Address = new AddressCreateRequest()
                {
                    Cep = "41700000",
                    CityId = Guid.NewGuid(),
                    Description = "test",
                    District = "test",
                    Number = "1"
                }
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), "CPF");

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count);
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("1234!@#$%")]
        [DataRow("AAABBBCCCCEEEE")]
        [DataRow("123456789012334454545aaa!@3")]
        [DataRow("test Invalid !@#$%")]
        public void InvalidCellphoneNumber(string phone)
        {
            var seed = new UserAgentCreateRequest()
            {
                CompanyId = Guid.NewGuid(),
                CompleteName = "test test",
                Cpf = "89235723060",
                Email = "test@test.com",
                IsActive = true,
                Password = "Test!123",
                CellphoneNumber = phone,
                CorporateCellphoneNumber = "5571999999999",
                Address = new AddressCreateRequest()
                {
                    Cep = "41700000",
                    CityId = Guid.NewGuid(),
                    Description = "test",
                    District = "test",
                    Number = "1"
                }
            };

            // Act
            var validationResult = _validator.Validate(seed);

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count);
            Assert.AreEqual(string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("CellphoneNumber")), validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("1234!@#$%")]
        [DataRow("AAABBBCCCCEEEE")]
        [DataRow("123456789012334454545aaa!@3")]
        [DataRow("test Invalid !@#$%")]
        public void InvalidCorporateCellphoneNumber(string phone)
        {
            var seed = new UserAgentCreateRequest()
            {
                CompanyId = Guid.NewGuid(),
                CompleteName = "test test",
                Cpf = "89235723060",
                Email = "test@test.com",
                IsActive = true,
                Password = "Test!123",
                CorporateCellphoneNumber = phone,
                CellphoneNumber  = "5571999999999",
                Address = new AddressCreateRequest()
                {
                    Cep = "41700000",
                    CityId = Guid.NewGuid(),
                    Description = "test",
                    District = "test",
                    Number = "1"
                }
            };

            // Act
            var validationResult = _validator.Validate(seed);

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count);
            Assert.AreEqual(string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("CorporateCellphoneNumber")), validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("1234!@#$%")]
        [DataRow("AAABBBCCCCEEEE")]
        [DataRow("senha invalida")]
        [DataRow("pas1234")]
        [DataRow("!@#$%¨&*()")]
        public void InvalidPassword(string pass)
        {
            var seed = new UserAgentCreateRequest()
            {
                CompanyId = Guid.NewGuid(),
                CompleteName = "test test",
                Cpf = "89235723060",
                Email = "test@test.com",
                IsActive = true,
                Password = pass,
                CorporateCellphoneNumber = "5571999999999",
                CellphoneNumber = "5571999999999",
                Address = new AddressCreateRequest()
                {
                    Cep = "41700000",
                    CityId = Guid.NewGuid(),
                    Description = "test",
                    District = "test",
                    Number = "1"
                }
            };

            // Act
            var validationResult = _validator.Validate(seed);

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count);
            Assert.AreEqual(resource.GetMessage("PasswordOutPattern"), validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void CompanyIdRequired()
        {
            var seed = new UserAgentCreateRequest()
            {
                CompanyId = null,
                CompleteName = "test test",
                Cpf = "89235723060",
                Email = "test@test.com",
                IsActive = true,
                Password = "Test!123",
                CorporateCellphoneNumber = "5571999999999",
                CellphoneNumber = "5571999999999",
                Address = new AddressCreateRequest()
                {
                    Cep = "41700000",
                    CityId = Guid.NewGuid(),
                    Description = "test",
                    District = "test",
                    Number = "1"
                }
            };

            // Act
            var validationResult = _validator.Validate(seed);

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("Company")), validationResult.Errors.Single().ErrorMessage);
        }
    }
}

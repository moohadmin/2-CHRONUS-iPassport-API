using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.HealthUnit;
using iPassport.Api.Models.Validators.HealthUnit;
using iPassport.Application.Resources;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace iPassport.Test.Controllers.Validator
{

    [TestClass]
    public class HealthUnitCreateRequestValidatorTest
    {
        HealthUnitCreateRequestValidator _validator;
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
                new HealthUnitCreateRequest()
                {
                    Cnpj = "57807521000172",
                    Name = "test",
                    Address = new AddressCreateRequest()
                    {
                        Cep = "41000000",
                        CityId = Guid.NewGuid(),
                        Description = "test",
                        District = "test",
                        Number = "12"
                    },
                    CompanyId = Guid.NewGuid(),
                    Email = "test@test.com",
                    Ine = "0123456789",
                    IsActive = true,
                    Responsible = new HealthUnitResponsibleCreateRequest()
                    {
                        Name = "Responsible Name",
                        Occupation = "Occupation",
                        MobilePhone = "5571986865544",
                        Landline = "557133335555"
                    },
                    TypeId = Guid.NewGuid()
                }).IsValid);
        }

        [TestMethod]
        [DataRow("1234")]
        [DataRow("AAABBBCCCCEEEE")]
        [DataRow("123456789012334454545")]
        [DataRow("test Invalid !#$%")]
        public void InvalidEmail(string email)
        {
            var seed = new HealthUnitCreateRequest()
            {
                Cnpj = "57807521000172",
                Name = "test",
                Address = new AddressCreateRequest()
                {
                    Cep = "41000000",
                    CityId = Guid.NewGuid(),
                    Description = "test",
                    District = "test",
                    Number = "12"
                },
                CompanyId = Guid.NewGuid(),
                Email = email,
                Ine = "0123456789",
                IsActive = true,
                Responsible = new HealthUnitResponsibleCreateRequest()
                {
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                TypeId = Guid.NewGuid()
            };

            // Act
            var validationResult = _validator.Validate(seed);

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("Email")), validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void TypeIdRequired()
        {
            var seed = new HealthUnitCreateRequest()
            {
                Cnpj = "57807521000172",
                Name = "test",
                Address = new AddressCreateRequest()
                {
                    Cep = "41000000",
                    CityId = Guid.NewGuid(),
                    Description = "test",
                    District = "test",
                    Number = "12"
                },
                CompanyId = Guid.NewGuid(),
                Email = "test@test.com",
                Ine = "0123456789",
                IsActive = true,
                Responsible = new HealthUnitResponsibleCreateRequest()
                {
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                TypeId = null
            };

            // Act
            var validationResult = _validator.Validate(seed);

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("Type")), validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void CompanyIdRequired()
        {
            var seed = new HealthUnitCreateRequest()
            {
                Cnpj = "57807521000172",
                Name = "test",
                Address = new AddressCreateRequest()
                {
                    Cep = "41000000",
                    CityId = Guid.NewGuid(),
                    Description = "test",
                    District = "test",
                    Number = "12"
                },
                CompanyId = null,
                Email = "test@test.com",
                Ine = "0123456789",
                IsActive = true,
                Responsible = new HealthUnitResponsibleCreateRequest()
                {
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                TypeId = Guid.NewGuid()
            };

            // Act
            var validationResult = _validator.Validate(seed);

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("Company")), validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("1234")]
        [DataRow("1234!@#$%")]
        [DataRow("123456789012334454545")]
        [DataRow("123456789012334454545aaa!@3")]
        [DataRow("test Invalid !@#$%")]
        public void InvalidCnpj(string cnpj)
        {
            var seed = new HealthUnitCreateRequest()
            {
                Cnpj = cnpj,
                Name = "test",
                Address = new AddressCreateRequest()
                {
                    Cep = "41000000",
                    CityId = Guid.NewGuid(),
                    Description = "test",
                    District = "test",
                    Number = "12"
                },
                CompanyId = Guid.NewGuid(),
                Email = "test@test.com",
                Ine = "0123456789",
                IsActive = true,
                Responsible = new HealthUnitResponsibleCreateRequest()
                {
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                TypeId = Guid.NewGuid()
            };

            // Act
            var validationResult = _validator.Validate(seed);

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(string.Format(resource.GetMessage("InvalidField"), "CNPJ"), validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("1234!@#$%")]
        [DataRow("123456789012334454545")]
        public void InvalidIne(string ine)
        {
            var seed = new HealthUnitCreateRequest()
            {
                Cnpj = "57807521000172",
                Name = "test",
                Address = new AddressCreateRequest()
                {
                    Cep = "41000000",
                    CityId = Guid.NewGuid(),
                    Description = "test",
                    District = "test",
                    Number = "12"
                },
                CompanyId = Guid.NewGuid(),
                Email = "test@test.com",
                Ine = ine,
                IsActive = true,
                Responsible = new HealthUnitResponsibleCreateRequest()
                {
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                TypeId = Guid.NewGuid()
            };

            // Act
            var validationResult = _validator.Validate(seed);

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(string.Format(resource.GetMessage("InvalidField"), "INE"), validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("1234!@#$%")]
        [DataRow("AAABBBCCCCEEEE")]
        [DataRow("123456789012334454545aaa!@3")]
        [DataRow("test Invalid !@#$%")]
        public void InvalidResponsiblePersonPhone(string phone)
        {
            var seed = new HealthUnitCreateRequest()
            {
                Cnpj = "57807521000172",
                Name = "test",
                Address = new AddressCreateRequest()
                {
                    Cep = "41000000",
                    CityId = Guid.NewGuid(),
                    Description = "test",
                    District = "test",
                    Number = "12"
                },
                CompanyId = Guid.NewGuid(),
                Email = "test@test.com",
                Ine = "0123456789",
                IsActive = true,
                Responsible = new HealthUnitResponsibleCreateRequest()
                {
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    MobilePhone = phone,
                    Landline = "557133335555"
                },
                TypeId = Guid.NewGuid()
            };

            // Act
            var validationResult = _validator.Validate(seed);

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("ResponsiblePersonMobilePhone")), validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("1234!@#$%")]
        [DataRow("AAABBBCCCCEEEE")]
        [DataRow("123456789012334454545aaa!@3")]
        [DataRow("test Invalid !@#$%")]
        public void InvalidResponsiblePersonLandline(string landlineNumber)
        {
            var seed = new HealthUnitCreateRequest()
            {
                Cnpj = "57807521000172",
                Name = "test",
                Address = new AddressCreateRequest()
                {
                    Cep = "41000000",
                    CityId = Guid.NewGuid(),
                    Description = "test",
                    District = "test",
                    Number = "12"
                },
                CompanyId = Guid.NewGuid(),
                Email = "test@test.com",
                Ine = "0123456789",
                IsActive = true,
                Responsible = new HealthUnitResponsibleCreateRequest()
                {
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    MobilePhone = "5571986865544",
                    Landline = landlineNumber
                },
                TypeId = Guid.NewGuid()
            };

            // Act
            var validationResult = _validator.Validate(seed);

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("ResponsiblePersonLandline")), validationResult.Errors.Single().ErrorMessage);
        }
    }
}

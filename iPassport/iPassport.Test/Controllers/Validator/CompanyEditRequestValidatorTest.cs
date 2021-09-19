using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.Company;
using iPassport.Api.Models.Validators.Company;
using iPassport.Application.Resources;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace iPassport.Test.Controllers.Validator
{
    [TestClass]
    public class CompanyEditRequestValidatorTest
    {
        CompanyEditRequestValidator _validator;
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
                new CompanyEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Teste",
                    TradeName = "TradeName",
                    Cnpj = "59850374000149",
                    Address = new AddressEditRequest()
                    {
                        Id = Guid.NewGuid(),
                        Description = "Description",
                        Number = "10",
                        Cep = "43700000",
                        District = "District",
                        CityId = Guid.NewGuid()
                    },
                    SegmentId = Guid.NewGuid(),
                    IsHeadquarters = false,
                    ParentId = Guid.NewGuid(),
                    Responsible = new CompanyResponsibleEditRequest()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Responsible Name",
                        Occupation = "Occupation",
                        Email = "teste@teste.com",
                        MobilePhone = "5571986865544",
                        Landline = "557133335555"
                    },
                    IsActive = true
                }).IsValid);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("   ")]
        public void RequiredName(string name)
        {
            //Arrange
            var seed = new CompanyEditRequest()
            {
                Id = Guid.NewGuid(),
                Name = name,
                TradeName = "TradeName",
                Cnpj = "59850374000149",
                Address = new AddressEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Description = "Description",
                    Number = "10",
                    Cep = "43700000",
                    District = "District",
                    CityId = Guid.NewGuid()
                },
                SegmentId = Guid.NewGuid(),
                IsHeadquarters = false,
                ParentId = Guid.NewGuid(),
                Responsible = new CompanyResponsibleEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    Email = "teste@teste.com",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                IsActive = true
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("Name"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("   ")]
        public void RequiredCnpj(string name)
        {
            //Arrange
            var seed = new CompanyEditRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                TradeName = "TradeName",
                Cnpj = name,
                Address = new AddressEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Description = "Description",
                    Number = "10",
                    Cep = "43700000",
                    District = "District",
                    CityId = Guid.NewGuid()
                },
                SegmentId = Guid.NewGuid(),
                IsHeadquarters = false,
                ParentId = Guid.NewGuid(),
                Responsible = new CompanyResponsibleEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    Email = "teste@teste.com",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                IsActive = true
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("CNPJ"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("00000000000000")]
        [DataRow("11111111111111")]
        [DataRow("22222222222222")]
        [DataRow("33333333333333")]
        [DataRow("44444444444444")]
        [DataRow("55555555555555")]
        [DataRow("66666666666666")]
        [DataRow("77777777777777")]
        [DataRow("88888888888888")]
        [DataRow("99999999999999")]
        [DataRow("71.106.025/0001-12")]
        [DataRow("66379676000156")]
        [DataRow("663796760")]
        [DataRow("6637967600Abc6")]
        public void InvalidCnpj(string name)
        {
            //Arrange
            var seed = new CompanyEditRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                TradeName = "TradeName",
                Cnpj = name,
                Address = new AddressEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Description = "Description",
                    Number = "10",
                    Cep = "43700000",
                    District = "District",
                    CityId = Guid.NewGuid()
                },
                SegmentId = Guid.NewGuid(),
                IsHeadquarters = false,
                ParentId = Guid.NewGuid(),
                Responsible = new CompanyResponsibleEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    Email = "teste@teste.com",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                IsActive = true
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("CNPJ"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void RequiredAddress()
        {
            //Arrange
            var seed = new CompanyEditRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                TradeName = "TradeName",
                Cnpj = "90662086000100",
                Address = null,
                SegmentId = Guid.NewGuid(),
                IsHeadquarters = false,
                ParentId = Guid.NewGuid(),
                Responsible = new CompanyResponsibleEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    Email = "teste@teste.com",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                IsActive = true
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("Address"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("text")]
        [DataRow("45as55bb")]
        [DataRow("43.700-000")]
        [DataRow("4370000")]
        public void InvalidAddressCep(string cep)
        {
            //Arrange
            var seed = new CompanyEditRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                TradeName = "TradeName",
                Cnpj = "59850374000149",
                Address = new AddressEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Description = "Description",
                    Number = "10",
                    Cep = cep,
                    District = "District",
                    CityId = Guid.NewGuid()
                },
                SegmentId = Guid.NewGuid(),
                IsHeadquarters = false,
                ParentId = Guid.NewGuid(),
                Responsible = new CompanyResponsibleEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    Email = "teste@teste.com",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                IsActive = true
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("Cep"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]        
        [DataRow("43.700-000")]
        [DataRow("-4370000")]
        public void ValidAddressNumber(string number)
        {
            //Arrange
            var seed = new CompanyEditRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                TradeName = "TradeName",
                Cnpj = "59850374000149",
                Address = new AddressEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Description = "Description",
                    Number = number,
                    Cep = "43700000",
                    District = "District",
                    CityId = Guid.NewGuid()
                },
                SegmentId = Guid.NewGuid(),
                IsHeadquarters = false,
                ParentId = Guid.NewGuid(),
                Responsible = new CompanyResponsibleEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    Email = "teste@teste.com",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                IsActive = true
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("Number"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void RequiredSegmentId()
        {
            //Arrange
            var seed = new CompanyEditRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                TradeName = "TradeName",
                Cnpj = "59850374000149",
                Address = new AddressEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Description = "Description",
                    Number = "10",
                    Cep = "43700000",
                    District = "District",
                    CityId = Guid.NewGuid()
                },
                SegmentId = null,
                IsHeadquarters = false,
                ParentId = Guid.NewGuid(),
                Responsible = new CompanyResponsibleEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    Email = "teste@teste.com",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                IsActive = true
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("Segment"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void RequiredIsActive()
        {
            //Arrange
            var seed = new CompanyEditRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                TradeName = "TradeName",
                Cnpj = "59850374000149",
                Address = new AddressEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Description = "Description",
                    Number = "10",
                    Cep = "43700000",
                    District = "District",
                    CityId = Guid.NewGuid()
                },
                SegmentId = Guid.NewGuid(),
                IsHeadquarters = false,
                ParentId = Guid.NewGuid(),
                Responsible = new CompanyResponsibleEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Responsible Name",
                    Occupation = "Occupation",
                    Email = "teste@teste.com",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                IsActive = null
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("IsActive"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void RequiredResponsible()
        {
            //Arrange
            var seed = new CompanyEditRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                TradeName = "TradeName",
                Cnpj = "59850374000149",
                Address = new AddressEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Description = "Description",
                    Number = "10",
                    Cep = "43700000",
                    District = "District",
                    CityId = Guid.NewGuid()
                },
                SegmentId = Guid.NewGuid(),
                IsHeadquarters = false,
                ParentId = Guid.NewGuid(),
                Responsible = null,
                IsActive = true
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("Responsible"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("   ")]
        public void RequiredResponsibleName(string name)
        {
            //Arrange
            var seed = new CompanyEditRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                TradeName = "TradeName",
                Cnpj = "59850374000149",
                Address = new AddressEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Description = "Description",
                    Number = "10",
                    Cep = "43700000",
                    District = "District",
                    CityId = Guid.NewGuid()
                },
                SegmentId = Guid.NewGuid(),
                IsHeadquarters = false,
                ParentId = Guid.NewGuid(),
                Responsible = new CompanyResponsibleEditRequest()
                {
                    Name = name,
                    Id = Guid.NewGuid(),
                    Occupation = "Occupation",
                    Email = "teste@teste.com",
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                IsActive = true
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("RequiredField"), resource.GetMessage("ResponsiblePersonName"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("test")]
        [DataRow("Test@")]
        [DataRow("*&¨&¨$%$&¨&¨(*&(*.)(*")]
        [DataRow("Test.com")]
        public void ValidResponsibleEmail(string email)
        {
            //Arrange
            var seed = new CompanyEditRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                TradeName = "TradeName",
                Cnpj = "59850374000149",
                Address = new AddressEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Description = "Description",
                    Number = "10",
                    Cep = "43700000",
                    District = "District",
                    CityId = Guid.NewGuid()
                },
                SegmentId = Guid.NewGuid(),
                IsHeadquarters = false,
                ParentId = Guid.NewGuid(),
                Responsible = new CompanyResponsibleEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Occupation = "Occupation",
                    Email = email,
                    MobilePhone = "5571986865544",
                    Landline = "557133335555"
                },
                IsActive = true
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("ResponsiblePersonEmail"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("test")]
        [DataRow("+5571988445522")]
        [DataRow("123a")]
        [DataRow("123 43")]
        [DataRow("5500988445522")]
        [DataRow("550098445522")]
        [DataRow("5571488445522")]
        [DataRow("55714884455222")]
        [DataRow("5571988445522&")]
        public void ValidResponsibleMobileNumber(string mobileNumber)
        {
            //Arrange
            var seed = new CompanyEditRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                TradeName = "TradeName",
                Cnpj = "59850374000149",
                Address = new AddressEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Description = "Description",
                    Number = "10",
                    Cep = "43700000",
                    District = "District",
                    CityId = Guid.NewGuid()
                },
                SegmentId = Guid.NewGuid(),
                IsHeadquarters = false,
                ParentId = Guid.NewGuid(),
                Responsible = new CompanyResponsibleEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Occupation = "Occupation",
                    Email = "teste@teste.com",
                    MobilePhone = mobileNumber,
                    Landline = "557133335555"
                },
                IsActive = true
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("ResponsiblePersonMobilePhone"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("test")]
        [DataRow("+5571988445522")]
        [DataRow("123a")]
        [DataRow("123 43")]
        [DataRow("5500988445522")]
        [DataRow("550038445522")]
        [DataRow("55714884455")]
        [DataRow("55714884455222")]
        [DataRow("5571988445522&")]
        public void ValidResponsibleLandline(string landlineNumber)
        {
            //Arrange
            var seed = new CompanyEditRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                TradeName = "TradeName",
                Cnpj = "59850374000149",
                Address = new AddressEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Description = "Description",
                    Number = "10",
                    Cep = "43700000",
                    District = "District",
                    CityId = Guid.NewGuid()
                },
                SegmentId = Guid.NewGuid(),
                IsHeadquarters = false,
                ParentId = Guid.NewGuid(),
                Responsible = new CompanyResponsibleEditRequest()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Occupation = "Occupation",
                    Email = "teste@teste.com",
                    MobilePhone = "5571988554477",
                    Landline = landlineNumber
                },
                IsActive = true
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("ResponsiblePersonLandline"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }
    }
}

using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Validators.Plans;
using iPassport.Application.Resources;
using iPassport.Domain.Enums;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace iPassport.Test.Controllers.Validator
{
    [TestClass]
    public class PinRequestValidatorTest
    {
        PinRequestValidator _validator;
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
                new PinRequest()
                {
                    Doctype = EDocumentType.CPF,
                    Document = "57862423585",
                    Phone = "5571999999944"
                }).IsValid);
        }

        [TestMethod]
        [DataRow("123a1234")]
        [DataRow("aaaaaaaaaa")]
        [DataRow("!@#$%¨&")]
        [DataRow("12345678901234567")]
        public void InvalidCns(string cns)
        {
            var seed = new PinRequest()
            {
                Doctype = EDocumentType.CNS,
                Document = cns,
                Phone = "5571999999944"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("Cns"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("123a1234")]
        [DataRow("aaaaaaaaaa")]
        [DataRow("!@#$%¨&")]
        [DataRow("12345678901")]
        [DataRow("11111111111")]
        [DataRow("22222222222")]
        public void InvalidCpf(string cpf)
        {
            var seed = new PinRequest()
            {
                Doctype = EDocumentType.CPF,
                Document = cpf,
                Phone = "5571999999944"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("Cpf"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("123a1234")]
        [DataRow("aaaaaaaaaa")]
        [DataRow("!@#$%¨&")]
        [DataRow("a222222222a22")]
        [DataRow("ar222222222a22")]
        public void InvalidPassport(string passport)
        {
            var seed = new PinRequest()
            {
                Doctype = EDocumentType.Passport,
                Document = passport,
                Phone = "5571999999944"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("PassportDocument"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("!@#$%¨&")]
        [DataRow("1234567890123456")]
        [DataRow("a1234567890-1234")]
        public void InvalidRg(string rg)
        {
            var seed = new PinRequest()
            {
                Doctype = EDocumentType.RG,
                Document = rg,
                Phone = "5571999999944"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("Rg"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("!@#$%¨&")]
        [DataRow("1234567890123456")]
        [DataRow("a1234567890-1234")]
        public void InvalidInternationalDocument(string internationalDocument)
        {
            var seed = new PinRequest()
            {
                Doctype = EDocumentType.InternationalDocument,
                Document = internationalDocument,
                Phone = "5571999999944"
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("InternationalDocument"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }

        [TestMethod]
        [DataRow("!@#$%¨&")]
        [DataRow("a1234567890-1234")]
        [DataRow("5571999999")]
        [DataRow("+55719999999")]
        public void InvalidPhone(string phone)
        {
            var seed = new PinRequest()
            {
                Doctype = EDocumentType.CPF,
                Document = "57862423585",
                Phone = phone
            };

            // Act
            var validationResult = _validator.Validate(seed);

            var expetedMessage = string.Format(resource.GetMessage("InvalidField"), resource.GetMessage("Telephone"));

            // Assert
            Assert.AreEqual(1, validationResult.Errors.Count());
            Assert.AreEqual(expetedMessage, validationResult.Errors.Single().ErrorMessage);
        }
    }
}

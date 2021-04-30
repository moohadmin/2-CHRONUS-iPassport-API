using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.User;
using iPassport.Api.Models.Validators.Users;
using iPassport.Application.Resources;
using iPassport.Test.Settings.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
    }
}

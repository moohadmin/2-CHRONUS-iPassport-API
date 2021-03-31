using iPassport.Application.Resources;
using iPassport.Domain.Dtos.DtoValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPassport.Test.Services.DtoValidator
{
    [TestClass]
    public class UserImportDtoValidatorTest
    {
        UserImportDtoValidator _validator;
        public Resource resource { get; private set; }

        public UserImportDtoValidatorTest() { }

        [TestInitialize]
        public void Setup()
        {
            // _validator = new()
        }
    }
}

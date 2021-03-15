using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Settings.Seeds
{
    public static class UserDiseaseTestSeed
    {
        public static PagedData<UserDiseaseTest> GetUserDiseaseTests()
        {
            var tests = new List<UserDiseaseTest>()
            {
                new UserDiseaseTest(Guid.NewGuid(), Guid.NewGuid(), false, DateTime.UtcNow, DateTime.UtcNow.AddDays(3)),
                new UserDiseaseTest(Guid.NewGuid(), Guid.NewGuid(), true, DateTime.UtcNow, DateTime.UtcNow.AddDays(1)),
                new UserDiseaseTest(Guid.NewGuid(), Guid.NewGuid(), false, DateTime.UtcNow, DateTime.UtcNow.AddDays(7)),
                new UserDiseaseTest(Guid.NewGuid(), Guid.NewGuid(), true, DateTime.UtcNow, DateTime.UtcNow.AddDays(5)),
            };

            return new PagedData<UserDiseaseTest>() { Data = tests };
        }
    }
}

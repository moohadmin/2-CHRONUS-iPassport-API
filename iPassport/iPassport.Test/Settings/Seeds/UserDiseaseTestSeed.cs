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
                new UserDiseaseTest(Guid.NewGuid(), false, DateTime.UtcNow, DateTime.UtcNow.AddDays(3),"Nome Test"),
                new UserDiseaseTest(Guid.NewGuid(), true, DateTime.UtcNow, DateTime.UtcNow.AddDays(1),"Nome Test"),
                new UserDiseaseTest(Guid.NewGuid(), false, DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"Nome Test"),
                new UserDiseaseTest(Guid.NewGuid(), true, DateTime.UtcNow, DateTime.UtcNow.AddDays(5),"Nome Test"),
            };

            return new PagedData<UserDiseaseTest>() { Data = tests };
        }
    }
}

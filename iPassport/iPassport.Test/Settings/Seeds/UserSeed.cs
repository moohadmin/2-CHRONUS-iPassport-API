using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPassport.Test.Seeds
{
    public static class UserSeed
    {
        public static UserDetails GetUserDetails() =>
            new UserDetails(Guid.NewGuid())
            {
                Plan = PlanSeed.GetPlans().FirstOrDefault(),
                UserVaccines = new List<UserVaccine>()
                {
                    new UserVaccine(DateTime.UtcNow, 1, Guid.NewGuid(), Guid.NewGuid())
                }
            };

        public static Users GetUsers() =>
            new Users("test", "test", "test", "test", "test", DateTime.UtcNow, "test", "test", "test", "test", "test", null, "test", "test", "test", "test", 1);

        public static Users GetUserAgent() =>
            new Users("test", "test", "test", "test", "test", DateTime.UtcNow, "test", "test", "test", "test", "test", null, "test", "test", "test", "test", 2);
    }
}

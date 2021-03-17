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
                    new UserVaccine(DateTime.UtcNow, 1, Guid.NewGuid(), Guid.NewGuid(), "test", "test", "test", "test", Guid.NewGuid())
                }
            };

        public static Users GetUser() =>
            new Users("test", "test", "test", "test", "test", DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "test", null, null, "test", "test", "test", "test", Guid.NewGuid(), 1);

        public static Users GetUserAgent() =>
            new Users("test", "test", "test", "test", "test", DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "test", null, null, "test", "test", "test", "test", Guid.NewGuid(), 2);

        public static IList<Users> GetUsers()
        {
            return new List<Users>()
            {
                new Users("test", "test", "test", "test", "test", DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "test", null, null, "test", "test", "test", "test", Guid.NewGuid(), 2),
                new Users("test", "test", "test", "test", "test", DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "test", null, null, "test", "test", "test", "test", Guid.NewGuid(), 1)

        };
        }

        public static PagedData<Users> GetPagedUsers()
        {
            return new PagedData<Users>() { Data = GetUsers() };
        }
    }
}

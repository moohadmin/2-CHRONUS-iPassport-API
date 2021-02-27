using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using System;
using System.Linq;

namespace iPassport.Test.Seeds
{
    public static class UserSeed
    {
        public static UserDetails GetUserDetails() => 
            new UserDetails(Guid.NewGuid())
            {
                Plan = PlanSeed.GetPlans().FirstOrDefault()
            };

        public static Users GetUsers() =>
            new Users("test", "test", "test", "test", "test", DateTime.Now, "test", "test", "test", "test", "test", null, "test", "test", "test", "test");
    }
}

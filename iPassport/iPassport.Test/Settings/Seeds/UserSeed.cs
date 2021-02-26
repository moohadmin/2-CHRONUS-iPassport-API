using iPassport.Domain.Entities;
using System;

namespace iPassport.Test.Seeds
{
    public static class UserSeed
    {
        public static UserDetails GetUserDetails() =>
            new UserDetails(Guid.NewGuid(), "teste", "teste", "teste", "teste", "teste", DateTime.Now, "teste", "teste", "teste", "teste", "teste", "teste", Guid.NewGuid());
        public static UserDetails GetUserDetailsWithoutPhoto() =>
            new UserDetails(Guid.NewGuid(), "teste", "teste", "teste", "teste", "teste", DateTime.Now, "teste", "teste", "teste", "teste", "teste", "", Guid.NewGuid());
    }
}

using iPassport.Domain.Entities;
using System.Collections.Generic;

namespace iPassport.Test.Settings.Seeds
{
    public static class ProfileSeed
    {
        public static IList<Profile> GetProfiles() =>
            new List<Profile>()
            {
                new Profile("profile test 1", "1"),
                new Profile("profile test 2", "2"),
                new Profile("profile test 3", "3"),
                new Profile("profile test 4", "4"),
                new Profile("profile test 5", "5")
            };

        public static Profile Get() =>
                new Profile("profile test 1", "1");
    }
}

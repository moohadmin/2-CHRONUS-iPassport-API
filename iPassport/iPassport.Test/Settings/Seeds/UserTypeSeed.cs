using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using System.Collections.Generic;

namespace iPassport.Test.Settings.Seeds
{
    public static class UserTypeSeed
    {
        public static IList<UserType> GetUserTypes() =>
            new List<UserType>()
            {
                GetAdmin(),
                GetAgent(),
                GetCitizen()
            };

        public static UserType GetAdmin() => new UserType(EUserType.Admin.ToString(), (int)EUserType.Admin);
        public static UserType GetAgent() => new UserType(EUserType.Agent.ToString(), (int)EUserType.Agent);
        public static UserType GetCitizen() => new UserType(EUserType.Citizen.ToString(), (int)EUserType.Citizen);
        public static UserType Get(EUserType userType) => new UserType(userType.ToString(), (int)userType);


    }
}

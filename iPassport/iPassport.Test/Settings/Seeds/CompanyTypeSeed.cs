using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using System.Collections.Generic;

namespace iPassport.Test.Settings.Seeds
{
    public static class CompanyTypeSeed
    {
        public static IList<CompanyType> GetCompanyTypes() =>
            new List<CompanyType>()
            {
                GetGovernment(),
                GetPrivate()
            };
        public static CompanyType GetGovernment() => new CompanyType("Government-test", (int)ECompanyType.Government);
        public static CompanyType GetPrivate() => new CompanyType("Private-test", (int)EHealthUnitType.Private);
    }
}

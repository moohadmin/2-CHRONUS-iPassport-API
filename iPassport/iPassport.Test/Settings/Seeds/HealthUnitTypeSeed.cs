using iPassport.Domain.Entities;
using iPassport.Domain.Enums;

namespace iPassport.Test.Settings.Seeds
{
    public static class HealthUnitTypeSeed
    {
        public static HealthUnitType GetHealthUnitTypePublic() => new HealthUnitType("public-test", (int)EHealthUnitType.Public);
        public static HealthUnitType GetHealthUnitTypePrivate() => new HealthUnitType("private-test", (int)EHealthUnitType.Private);
    }
}

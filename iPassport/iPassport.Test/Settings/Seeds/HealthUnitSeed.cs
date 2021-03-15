using iPassport.Domain.Entities;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class HealthUnitSeed
    {
        public static HealthUnit GetCountry() => new HealthUnit();

        public static IList<HealthUnit> GetHealthUnits()
        {
            return new List<HealthUnit>()
            {
                new HealthUnit(),
                new HealthUnit(),
                new HealthUnit()
               };
        }

        public static PagedData<HealthUnit> GetPaged()
        {
            return new PagedData<HealthUnit>() { Data = GetHealthUnits() };
        }
    }
}

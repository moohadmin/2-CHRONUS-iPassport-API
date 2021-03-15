using iPassport.Domain.Entities;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class BloodTypeSeed
    {
        public static BloodType Get() => new BloodType("A");

        public static IList<BloodType> GetBloodTypes()
        {
            return new List<BloodType>()
            {
                new BloodType("A"),
                new BloodType("B"),
                new BloodType("O+")
            };
        }

        public static PagedData<BloodType> GetPaged()
        {
            return new PagedData<BloodType>() { Data = GetBloodTypes() };
        }
    }
}

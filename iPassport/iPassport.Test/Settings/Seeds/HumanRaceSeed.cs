using iPassport.Domain.Entities;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class HumanRaceSeed
    {
        public static HumanRace Get() => new HumanRace("Amarela");

        public static IList<HumanRace> GetGenders()
        {
            return new List<HumanRace>()
            {
                new HumanRace("Amarela"),
                new HumanRace("Branca"),
                new HumanRace("Parda")
            };
        }

        public static PagedData<HumanRace> GetPaged()
        {
            return new PagedData<HumanRace>() { Data = GetGenders() };
        }
    }
}

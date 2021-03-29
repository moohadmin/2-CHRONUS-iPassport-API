using iPassport.Domain.Entities;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class PriorityGroupSeed
    {
        public static PriorityGroup Get() => new PriorityGroup("Pessoas com deficiência institucionalizadas;");

        public static IList<PriorityGroup> GetPriorityGroups()
        {
            return new List<PriorityGroup>()
            {
                new PriorityGroup("Pessoas com 60 anos ou mais institucionalizadas;"),
                new PriorityGroup("Povos indígenas vivendo em terras indígenas;"),
                new PriorityGroup("Trabalhadores de saúde;")
            };
        }

        public static PagedData<PriorityGroup> GetPaged()
        {
            return new PagedData<PriorityGroup>() { Data = GetPriorityGroups() };
        }
    }
}

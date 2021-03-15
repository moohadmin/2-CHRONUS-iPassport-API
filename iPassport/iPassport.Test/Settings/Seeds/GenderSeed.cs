using iPassport.Domain.Entities;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class GenderSeed
    {
        public static Gender Get() => new Gender("Masculino");

        public static IList<Gender> GetGenders()
        {
            return new List<Gender>()
            {
                new Gender("Masculino"),
                new Gender("Feminino"),
                new Gender("Não Binário")
            };
        }

        public static PagedData<Gender> GetPaged()
        {
            return new PagedData<Gender>() { Data = GetGenders() };
        }
    }
}

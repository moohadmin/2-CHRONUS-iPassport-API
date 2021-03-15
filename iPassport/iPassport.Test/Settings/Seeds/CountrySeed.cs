using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class CountrySeed
    {
        public static Country GetCountry() => new Country("Brasil", "BR", "BRA",null);


        public static IList<Country> GetCountries()
        {
            return new List<Country>()
            {
                new Country("Brasil", "BR", "BRA",null),
                new Country("França", "FR", "FRA",null)
            };
        }

        public static PagedData<Country> GetPaged()
        {
            

            return new PagedData<Country>() { Data = GetCountries() };
        }
    }
}

using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class CitySeed
    {
        public static City GetState() => new City("Salvador", "SSA", 123, Guid.NewGuid(), 10);


        public static IList<City> GetCities()
        {
            return new List<City>()
            {
                new City("Salvador", "ssa", 123, Guid.NewGuid(), 10),
                new City("Rio de Janeiro", "RJ", 1234, Guid.NewGuid(), null),
            };
        }

        public static PagedData<City> GetPaged()
        {
            

            return new PagedData<City>() { Data = GetCities() };
        }
    }
}

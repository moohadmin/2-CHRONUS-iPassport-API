using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class VaccineSeed
    {
        public static PagedData<Vaccine> GetPagedVaccines()
        {
            var vac = new List<Vaccine>()
            {
                new Vaccine("vacina-teste", Guid.NewGuid(), 2, 360, 20),
                new Vaccine("vacina-teste1", Guid.NewGuid(), 2, 360, 20),
                new Vaccine("vacina-teste2", Guid.NewGuid(), 3, 460, 30),
                new Vaccine("vacina-teste3", Guid.NewGuid(), 1, 660, 10),
            };
           
            return new PagedData<Vaccine>() { Data = vac};
        }
    }
}

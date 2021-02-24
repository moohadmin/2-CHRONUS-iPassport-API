using iPassport.Domain.Entities;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class VaccineSeed
    {
        public static PagedData<Vaccine> GetPagedVaccines()
        {
            var vac = new List<Vaccine>()
            {
                new Vaccine("vacina-teste", "lab-teste", 2, 360, 20),
                new Vaccine("vacina-teste1", "lab-teste1", 2, 360, 20),
                new Vaccine("vacina-teste2", "lab-teste2", 3, 460, 30),
                new Vaccine("vacina-teste3", "lab-teste3", 1, 660, 10),
            };
           
            return new PagedData<Vaccine>() { Data = vac};
        }
    }
}

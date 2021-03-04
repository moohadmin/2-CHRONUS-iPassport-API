using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class VaccineSeed
    {
        public static IList<VaccineIndicatorDto> GetVaccineIndicatorDtos()
        {
            return new List<VaccineIndicatorDto>()
            {
                new VaccineIndicatorDto(){Count = 2, Disease = "Test", Dose = 1, ManufacturerId = Guid.NewGuid(), ManufacturerName = "test", VaccnineId = Guid.NewGuid(), VaccineName = "test" },
                new VaccineIndicatorDto(){Count = 2, Disease = "Test1", Dose = 1, ManufacturerId = Guid.NewGuid(), ManufacturerName = "test1", VaccnineId = Guid.NewGuid(), VaccineName = "test1" },
                new VaccineIndicatorDto(){Count = 2, Disease = "Test2", Dose = 1, ManufacturerId = Guid.NewGuid(), ManufacturerName = "test2", VaccnineId = Guid.NewGuid(), VaccineName = "test2" },
            };
        }

        public static IList<Vaccine> GetVaccines()
        {
            return new List<Vaccine>()
            { 
                new Vaccine("test", Guid.NewGuid(), 1, 1, 1, 1, 1),
                new Vaccine("test1", Guid.NewGuid(), 12, 13, 1, 1, 1),
                new Vaccine("test2", Guid.NewGuid(), 13, 14, 1, 1, 1),
                new Vaccine("test3", Guid.NewGuid(), 14, 15, 1, 1, 1),
                new Vaccine("test4", Guid.NewGuid(), 15, 16, 1, 1, 1)
            };
        }
    }
}

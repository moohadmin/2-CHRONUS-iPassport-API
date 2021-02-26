using iPassport.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class IndicatorSeed
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
    }
}

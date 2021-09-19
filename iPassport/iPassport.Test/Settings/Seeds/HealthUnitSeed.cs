using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class HealthUnitSeed
    {
        public static HealthUnit GetCountry() => new HealthUnit();

        public static IList<HealthUnit> GetHealthUnits()
        {
            return new List<HealthUnit>()
            {
                new HealthUnit(),
                new HealthUnit(),
                new HealthUnit()
               };
        }

        public static PagedData<HealthUnit> GetPaged()
        {
            return new PagedData<HealthUnit>() { Data = GetHealthUnits() };
        }

        public static HealthUnit GetHealthUnit() =>
            new HealthUnit().Create(new HealthUnitCreateDto()
            { 
                Address = new AddressCreateDto() {
                    Id = Guid.NewGuid(),
                    CityId = Guid.NewGuid()
                },
                Cnpj = "0000000000000",
                CompanyId = Guid.NewGuid(),
                Email = "test@test.com",
                Ine = "1234567890",
                IsActive = true,
                Name = "test",
                ResponsiblePersonName = "test",
                ResponsiblePersonOccupation = "test",
                ResponsiblePersonMobilePhone = "test",
                ResponsiblePersonLandline = "test",
                TypeId = Guid.NewGuid()
            });
    }
}

using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class UserVaccineSeed
    {
        public static PagedData<UserVaccineDetailsDto> GetPagedUserVaccines()
        {
            var vac = new List<UserVaccineDetailsDto>()
            {
                new UserVaccineDetailsDto()
                { 
                    VaccineId = Guid.NewGuid(),
                    Doses = new List<VaccineDoseDto>()
                        { 
                        new VaccineDoseDto(){ Dose = 1, VaccinationDate = DateTime.UtcNow, ExpirationTime = DateTime.UtcNow},
                        new VaccineDoseDto(){ Dose = 2, VaccinationDate = DateTime.UtcNow, ExpirationTime = DateTime.UtcNow},
                        new VaccineDoseDto(){ Dose = 3, VaccinationDate = DateTime.UtcNow, ExpirationTime = DateTime.UtcNow}
                    } 
                },
                new UserVaccineDetailsDto()
                {
                    VaccineId = Guid.NewGuid(),
                    Doses = new List<VaccineDoseDto>()
                        {
                        new VaccineDoseDto(){ Dose = 1, VaccinationDate = DateTime.UtcNow, ExpirationTime = DateTime.UtcNow},
                        new VaccineDoseDto(){ Dose = 2, VaccinationDate = DateTime.UtcNow, ExpirationTime = DateTime.UtcNow},
                        new VaccineDoseDto(){ Dose = 3, VaccinationDate = DateTime.UtcNow, ExpirationTime = DateTime.UtcNow}
                    }
                },
                new UserVaccineDetailsDto()
                {
                    VaccineId = Guid.NewGuid(),
                    Doses = new List<VaccineDoseDto>()
                        {
                        new VaccineDoseDto(){ Dose = 1, VaccinationDate = DateTime.UtcNow, ExpirationTime = DateTime.UtcNow},
                        new VaccineDoseDto(){ Dose = 2, VaccinationDate = DateTime.UtcNow, ExpirationTime = DateTime.UtcNow},
                        new VaccineDoseDto(){ Dose = 3, VaccinationDate = DateTime.UtcNow, ExpirationTime = DateTime.UtcNow}
                    }
                }
            };

            return new PagedData<UserVaccineDetailsDto>() { Data = vac };
        }
    }
}

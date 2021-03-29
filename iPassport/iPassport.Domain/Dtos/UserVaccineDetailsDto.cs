using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;

namespace iPassport.Domain.Dtos
{
    public class UserVaccineDetailsDto
    {
        public Guid VaccineId { get; set; }
        public string VaccineName { get; set; }
        public IEnumerable<VaccineDoseDto> Doses { get; set; }
        public int? RequiredDoses { get; set; }
        public int? ImmunizationTime { get; set; }
        public Guid UserId { get; set; }
        public EUserVaccineStatus Status { get; set; }
        public VaccineManufacturerDto Manufacturer { get; set; }
    }
}

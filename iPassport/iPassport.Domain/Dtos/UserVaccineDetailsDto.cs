using System;
using System.Collections.Generic;

namespace iPassport.Domain.Dtos
{
    public class UserVaccineDetailsDto
    {
        public Guid VaccineId { get; set; }
        public string VaccineName { get; set; }
        public IEnumerable<VaccineDoseDto> Doses { get; set; }
        public int RequiredDoses { get; set; }
        public int ImunizationTime { get; set; }
    }
}

using System;

namespace iPassport.Domain.Dtos
{
    public class UserVaccineCreateDto
    {
        public DateTime VaccinationDate { get; set; }
        public int Dose { get; set; }
        public Guid VaccineId { get; set; }
        public Guid UserId { get; set; }
    }
}

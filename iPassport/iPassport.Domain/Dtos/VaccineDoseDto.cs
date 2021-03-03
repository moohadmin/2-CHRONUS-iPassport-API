using System;

namespace iPassport.Domain.Dtos
{
    public class VaccineDoseDto
    {
        public int Dose { get; set; }
        public DateTime VaccinationDate { get; set; }
        public DateTime ValidDate { get; set; }
    }
}

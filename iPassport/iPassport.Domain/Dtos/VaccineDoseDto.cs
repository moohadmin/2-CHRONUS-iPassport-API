using System;

namespace iPassport.Domain.Dtos
{
    public class VaccineDoseDto
    {
        public Guid Id { get; set; }
        public int Dose { get; set; }
        public DateTime VaccinationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}

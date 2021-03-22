using System;

namespace iPassport.Domain.Dtos
{
    public class VaccineDoseDto
    {
        public Guid Id { get; set; }
        public int Dose { get; set; }
        public DateTime VaccinationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Batch { get; set; }
        public HealthUnitDto HealthUnit { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCpf { get; set; }
        public string EmployeeCoren { get; set; }
    }
}

using System;

namespace iPassport.Domain.Dtos
{
    public class UserVaccineEditDto
    {
        public Guid? Id { get; set; }
        public DateTime VaccinationDate { get; set; }
        public int Dose { get; set; }
        public Guid VaccineId { get; set; }
        public Guid UserId { get; set; }
        public string Batch { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCpf { get; set; }
        public string EmployeeCoren { get; set; }
        public Guid HealthUnitId { get; set; }
        
    }
}

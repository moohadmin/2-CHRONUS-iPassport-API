using iPassport.Domain.Enums;
using System;

namespace iPassport.Api.Models.Requests.User
{
    public class UserVaccineCreateRequest
    {
        public DateTime? VaccinationDate { get; set; }
        public int? Dose { get; set; }
        public Guid? Vaccine { get; set; }
        public string UnitName { get; set; }
        public string Batch { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCpf { get; set; }
        public string EmployeeCoren { get; set; }
        public Guid? City { get; set; }
        public EHealthUnityType? UnityType { get; set; }
    }
}
using iPassport.Domain.Enums;
using System;

namespace iPassport.Api.Models.Requests.User
{
    /// <summary>
    /// User Vaccine edit Request
    /// </summary>
    public class UserVaccineEditRequest
    {
        /// <summary>
        /// User Vaccine Id
        /// </summary>
        public Guid? UserVaccineId { get; set; }
        /// <summary>
        /// Vaccination Date
        /// </summary>
        public DateTime? VaccinationDate { get; set; }

        /// <summary>
        /// Dose
        /// </summary>
        public int? Dose { get; set; }

        /// <summary>
        /// Vaccine
        /// </summary>
        public Guid? Vaccine { get; set; }

        /// <summary>
        /// Batch
        /// </summary>
        public string Batch { get; set; }

        /// <summary>
        /// Employee Name
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Employee Cpf
        /// </summary>
        public string EmployeeCpf { get; set; }

        /// <summary>
        /// Employee Coren
        /// </summary>
        public string EmployeeCoren { get; set; }

        /// <summary>
        /// Health Unit Id
        /// </summary>
        public Guid? HealthUnitId { get; set; }
                
    }
}
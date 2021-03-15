using iPassport.Domain.Enums;
using System;

namespace iPassport.Api.Models.Requests.User
{
    /// <summary>
    /// User Vaccine Create Request
    /// </summary>
    public class UserVaccineCreateRequest
    {
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
        /// Unit Name
        /// </summary>
        public string UnitName { get; set; }

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
        /// City Id
        /// </summary>
        public Guid? City { get; set; }

        /// <summary>
        /// Unity Type
        /// </summary>
        public EHealthUnityType? UnityType { get; set; }
    }
}
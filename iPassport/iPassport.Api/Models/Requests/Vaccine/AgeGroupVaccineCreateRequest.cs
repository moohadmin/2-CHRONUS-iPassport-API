using iPassport.Domain.Enums;
using System;

namespace iPassport.Api.Models.Requests.Vaccine
{
    /// <summary>
    /// Age Group Vaccine Request
    /// </summary>
    public class AgeGroupVaccineCreateRequest
    {
        /// <summary>
        /// Period Type Id
        /// </summary>
        public EVaccinePeriodType? PeriodType { get; set; }
        /// <summary>
        /// Required Doses
        /// </summary>
        public int RequiredDoses { get; set; }
        /// <summary>
        /// Max Time Next Dose
        /// </summary>
        public int MaxTimeNextDose { get; set; }
        /// <summary>
        /// Min Time Next Dose
        /// </summary>
        public int MinTimeNextDose { get; set; }
        /// <summary>
        /// Inital Age Group
        /// </summary>
        public int InitalAgeGroup { get; set; }
        /// <summary>
        /// Final Age Group
        /// </summary>
        public int FinalAgeGroup { get; set; }
    }
}

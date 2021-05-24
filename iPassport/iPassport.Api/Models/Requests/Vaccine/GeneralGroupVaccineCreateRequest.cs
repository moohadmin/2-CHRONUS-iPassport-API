using iPassport.Domain.Enums;

namespace iPassport.Api.Models.Requests.Vaccine
{
    /// <summary>
    /// General Group Vaccine Create Request
    /// </summary>
    public class GeneralGroupVaccineCreateRequest
    {
        /// <summary>
        /// Period Type Id
        /// </summary>
        public EVaccinePeriodType? PeriodType { get; set; }
        /// <summary>
        /// Required Doses
        /// </summary>
        public int? RequiredDoses { get; set; }
        /// <summary>
        /// Max Time Next Dose
        /// </summary>
        public int? TimeNextDoseMax { get; set; }
        /// <summary>
        /// Min Time Next Dose
        /// </summary>
        public int? TimeNextDoseMin { get; set; }
    }
}

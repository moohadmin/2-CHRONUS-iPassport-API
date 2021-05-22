using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;

namespace iPassport.Api.Models.Requests.Vaccine
{
    /// <summary>
    /// Vaccine Create Request
    /// </summary>
    public class VaccineCreateRequest
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Manufacturer Id
        /// </summary>
        public Guid? Manufacturer { get; set; }
        /// <summary>
        /// Disease Ids
        /// </summary>
        public IEnumerable<Guid> Diseases { get; set; }
        /// <summary>
        /// Validity
        /// </summary>
        public int? ExpirationTimeInMonths { get; set; }
        /// <summary>
        /// Immunization Time In Days
        /// </summary>
        public int? ImmunizationTimeInDays { get; set; }
        /// <summary>
        /// Is Active?
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// Dosage Type Id
        /// </summary>
        public EVaccineDosageType? DosageType { get; set; }
        /// <summary>
        /// Age Group Vaccines List
        /// </summary>
        public IEnumerable<AgeGroupVaccineCreateRequest> AgeGroupVaccines { get; set; }
        /// <summary>
        /// General Group Vaccine
        /// </summary>
        public GeneralGroupVaccineCreateRequest GeneralGroupVaccine { get; set; }
    }

}

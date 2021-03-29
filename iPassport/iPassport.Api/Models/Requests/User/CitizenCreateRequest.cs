using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;

namespace iPassport.Api.Models.Requests.User
{
    /// <summary>
    /// Citizen Create Request
    /// </summary>
    public class CitizenCreateRequest
    {
        /// <summary>
        /// Complete Name
        /// </summary>
        public string CompleteName { get; set; }

        /// <summary>
        /// Gender Id
        /// </summary>
        public Guid? GenderId { get; set; }

        /// <summary>
        /// Cpf
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        /// Cns
        /// </summary>
        public string Cns { get; set; }

        /// <summary>
        /// Company Id
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// Occupation
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// Bond
        /// </summary>
        public string Bond { get; set; }

        /// <summary>
        /// Priority Group Id
        /// </summary>
        public Guid? PriorityGroupId { get; set; }

        /// <summary>
        /// Blood Type Id
        /// </summary>
        public Guid? BloodTypeId { get; set; }

        /// <summary>
        /// Human Race Id
        /// </summary>
        public Guid? HumanRaceId { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Was Covid Infected ?
        /// </summary>
        public bool? WasCovidInfected { get; set; }

        /// <summary>
        /// Number Of Vaccine Doses
        /// </summary>
        public int? NumberOfDoses { get; set; }

        /// <summary>
        /// Telephone
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public AddressCreateRequest Address { get; set; }

        /// <summary>
        /// Vaccine Doses
        /// </summary>
        public IList<UserVaccineCreateRequest> Doses {get; set;}

        /// <summary>
        /// Birthday
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Was Test Performed
        /// </summary>
        public bool? WasTestPerformed { get; set; }

        /// <summary>
        /// User Test
        /// </summary>
        public UserDiseaseTestCreateRequest Test { get; set; }
    }
}

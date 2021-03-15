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
        /// Gender
        /// </summary>
        public EGendersTypes? Gender { get; set; }

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
        /// Priority Group
        /// </summary>
        public string PriorityGroup { get; set; }

        /// <summary>
        /// Blood Type
        /// </summary>
        public string BloodType { get; set; }

        /// <summary>
        /// Breed
        /// </summary>
        public EBreedTypes? Breed { get; set; }

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
    }
}

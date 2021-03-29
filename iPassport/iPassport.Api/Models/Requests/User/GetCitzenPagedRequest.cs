using iPassport.Domain.Enums;
using System;

namespace iPassport.Api.Models.Requests.User
{
    /// <summary>
    /// Get Citzen Paged Request
    /// </summary>
    public class GetCitzenPagedRequest : PageFilterRequest
    {
        /// <summary>
        /// Name Initals / Name Parts
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// Document Types: 
        /// 1 - CPF;
        /// 2 - RG;
        /// 3 - Passport Document;
        /// 4 - CNS;
        /// 5 - International Document;
        /// </summary>
        public EDocumentType? DocumentType { get; set; }

        /// <summary>
        /// Document
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Telephone
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// CityId
        /// </summary>
        public Guid? CityId { get; set; }

        /// <summary>
        /// StateId
        /// </summary>
        public Guid? StateId { get; set; }

        /// <summary>
        /// CountryId
        /// </summary>
        public Guid? CountryId { get; set; }

        /// <summary>
        /// CompanyId
        /// </summary>
        public Guid? CompanyId { get; set; }


    }
}

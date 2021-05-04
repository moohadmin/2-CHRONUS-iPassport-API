using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Api.Models.Requests.User
{
    /// <summary>
    /// Get Agent Paged Request
    /// </summary>
    public class GetAgentPagedRequest : PageFilterRequest
    {
        /// <summary>
        /// Agent Login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Name Initals / Name Parts
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// Cpf
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        /// Company Id
        /// </summary>
        public Guid? CompanyId { get; set; }

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
    }
}

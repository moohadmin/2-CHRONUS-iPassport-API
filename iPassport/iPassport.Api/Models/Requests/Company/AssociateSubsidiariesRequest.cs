using System;
using System.Collections.Generic;

namespace iPassport.Api.Models.Requests.Company
{
    /// <summary>
    /// Associate Subsidiaries Request
    /// </summary>
    public class AssociateSubsidiariesRequest
    {
        /// <summary>
        /// Associate all Subs avaliables?
        /// </summary>
        public bool? AssociateAll { get; set; }
        /// <summary>
        /// Subsidiaries Ids
        /// </summary>
        public IEnumerable<Guid> Subsidiaries { get; set; }
    }
}

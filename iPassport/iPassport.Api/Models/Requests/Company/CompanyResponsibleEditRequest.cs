using System;

namespace iPassport.Api.Models.Requests.Company
{
    /// <summary>
    /// Company Responsible Edit Request
    /// </summary>
    public class CompanyResponsibleEditRequest
    {
        /// <summary>
        /// Responsible Id
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// Responsible Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Responsible Occupation
        /// </summary>
        public string Occupation { get; set; }
        /// <summary>
        /// Responsible Person Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Responsible Mobile Phone
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// Responsible Landline
        /// </summary>
        public string Landline { get; set; }
    }
}

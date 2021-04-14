using System;

namespace iPassport.Api.Models.Requests.Company
{

    /// <summary>
    /// Company Edit Request
    /// </summary>
    public class CompanyEditRequest
    {
        /// <summary>
        /// COmpany Id
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// Company Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Company Trade Name
        /// </summary>
        public string TradeName { get; set; }
        /// <summary>
        /// Company Cnpj
        /// </summary>
        public string Cnpj { get; set; }
        /// <summary>
        /// Company Address
        /// </summary>        
        public AddressEditRequest Address { get; set; }
        /// <summary>
        /// Company Segment
        /// </summary>
        public Guid? SegmentId { get; set; }
        /// <summary>
        /// Company Is Headquarters
        /// </summary>
        public bool? IsHeadquarters { get; set; }
        /// <summary>
        /// Parent Company
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// Company Responsible
        /// </summary>
        public CompanyResponsibleEditRequest Responsible { get; set; }
        /// <summary>
        /// Company Is Active
        /// </summary>
        public bool? IsActive { get; set; }
    }
}

using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Company View Model
    /// </summary>
    public class CompanyViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Company's Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Trade Name
        /// </summary>
        public string TradeName { get; set; }
        /// <summary>
        /// Company CNPJ
        /// </summary>
        public string Cnpj { get; set; }
        /// <summary>
        /// Address Id
        /// </summary>
        public Guid AddressId { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public AddressViewModel Address { get; set; }
        /// <summary>
        /// Company Segment
        /// </summary>
        public CompanySegmentViewModel Segment { get; set; }
        /// <summary>
        /// Parent Company
        /// </summary>
        public CompanyViewModel ParentCompany { get; set; }
        /// <summary>
        /// Company responsible
        /// </summary>
        public CompanyResponsibleViewModel Responsible { get; set; }
        /// <summary>
        /// Deactivation User
        /// </summary>
        public UserViewModel DeactivationUser { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
    }
}

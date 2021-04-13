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
        public CompanyViewModel Parent { get; set; }
    }
}

using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Health Unit View Model
    /// </summary>
    public class HealthUnitViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Health unit Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Health unit Cnpj
        /// </summary>
        public string Cnpj { get; set; }
        /// <summary>
        /// Health unit Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Health unit Responsible Person Name
        /// </summary>
        public string ResponsiblePersonName { get; set; }
        /// <summary>
        /// Health unit Responsible Person Phone
        /// </summary>
        public string ResponsiblePersonPhone { get; set; }
        /// <summary>
        /// Health unit Responsible Person Occupation 
        /// </summary>
        public string ResponsiblePersonOccupation { get; set; }
        /// <summary>
        /// Health unit Is Active
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Health unit Type
        /// </summary>
        public HealthUnitTypeViewModel Type { get; set; }
        /// <summary>
        /// Health unit AddressId
        /// </summary>
        public Guid? AddressId { get; set; }
        /// <summary>
        /// Health Unit Info Address
        /// </summary>
        public AddressViewModel Address {get; set;}

    }
}

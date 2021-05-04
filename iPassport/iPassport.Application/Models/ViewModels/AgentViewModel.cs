using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Citizen View Model
    /// </summary>
    public class AgentViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Citizen Full Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Citizen Full Name
        /// </summary>
        public string FullName { get; set; }
        
        /// <summary>
        /// Citizen CPF
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Citizen CompanyId
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// Address Id
        /// </summary>
        public Guid AddressId { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public AddressViewModel Address { get; set; }
    }
}

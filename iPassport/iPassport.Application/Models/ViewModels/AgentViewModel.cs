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
        /// Agent Full Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Agent Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Agent Cpf
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        /// Agent CompanyId
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// Agent Company Name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Address Id
        /// </summary>
        public Guid AddressId { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public AddressViewModel Address { get; set; }

        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }
    }
}

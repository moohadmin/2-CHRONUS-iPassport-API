using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Citizen View Model
    /// </summary>
    public class CitizenViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Citizen Full Name
        /// </summary>
        public string FullName { get; set; }
        
        /// <summary>
        /// Citizen CNS
        /// </summary>
        public string CNS { get; set; }
        
        /// <summary>
        /// Citizen CPF
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }
    }
}

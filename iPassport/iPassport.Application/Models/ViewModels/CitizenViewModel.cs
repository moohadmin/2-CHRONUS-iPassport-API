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
        /// Telephone
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Active Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// User was imported
        /// </summary>
        public bool WasImported { get; set; }
    }
}

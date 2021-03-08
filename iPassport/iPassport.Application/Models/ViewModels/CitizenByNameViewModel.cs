using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Citizen View Model
    /// </summary>
    public class CitizenByNameViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Citizen FullName
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Citizen CNS
        /// </summary>
        public string CNS { get; set; }
        /// <summary>
        /// CCitizen CPF
        /// </summary>
        public string CPF { get; set; }


    }
}

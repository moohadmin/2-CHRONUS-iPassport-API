using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// State View Model
    /// </summary>
    public class StateViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// State's Names
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// State's Acronym
        /// </summary>
        public string Acronym { get; set; }
        /// <summary>
        /// State Ibge COde
        /// </summary>
        public int IbgeCode { get; set; }
        /// <summary>
        /// State Population
        /// </summary>
        public long? Population { get; set; }

        /// <summary>
        /// Country Id
        /// </summary>
        public System.Guid CountryId { get; set; }
        
    }
}

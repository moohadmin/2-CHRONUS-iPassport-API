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
        /// Country's Names
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Country's Acronym
        /// </summary>
        public string Acronym { get; set; }
        /// <summary>
        /// Country External Code
        /// </summary>
        public int IbgeCode { get; set; }
        /// <summary>
        /// Country Population
        /// </summary>
        public long? Population { get; set; }

        /// <summary>
        /// Country Id
        /// </summary>
        public System.Guid CountryId { get; private set; }
        
    }
}

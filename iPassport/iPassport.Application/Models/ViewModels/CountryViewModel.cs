using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Country View Model
    /// </summary>
    public class CountryViewModel
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
        public string ExternalCode { get; set; }
        /// <summary>
        /// Country Population
        /// </summary>
        public long? Population { get; set; }

    }
}

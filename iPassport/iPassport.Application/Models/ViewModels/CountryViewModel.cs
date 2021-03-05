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
        /// Country's IBGE Code
        /// </summary>
        public int IbgeCode { get; set; }

    }
}

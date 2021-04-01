using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// HealthUnitType ViewModel
    /// </summary>
    public class HealthUnitTypeViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Health unit Type Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Identifyer code
        /// </summary>
        public int Identifyer { get; set; }

    }
}

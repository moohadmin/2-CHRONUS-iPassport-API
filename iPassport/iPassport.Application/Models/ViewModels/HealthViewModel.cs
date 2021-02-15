using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// GetAll Helth Model
    /// </summary>
    public class HealthViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid HealthId { get; set; }
        /// <summary>
        /// Status of Health
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Update date
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}

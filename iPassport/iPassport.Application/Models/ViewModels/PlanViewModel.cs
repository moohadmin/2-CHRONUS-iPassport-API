using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Plan View Model
    /// </summary>
    public class PlanViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Update date
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// Observation
        /// </summary>
        public string Observation { get; set; }
        /// <summary>
        /// Color starts in HexaDecimal without #
        /// </summary>
        public string ColorStart { get; set; }
        /// <summary>
        /// Color starts in HexaDecimal without #
        /// </summary>
        public string ColorEnd { get; set; }
        /// <summary>
        /// If Active
        /// </summary>
        public bool Active { get; set; }
    }
}

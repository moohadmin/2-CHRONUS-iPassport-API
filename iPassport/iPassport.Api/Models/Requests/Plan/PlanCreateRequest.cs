namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Plan Create Request model
    /// </summary>
    public class PlanCreateRequest
    {
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

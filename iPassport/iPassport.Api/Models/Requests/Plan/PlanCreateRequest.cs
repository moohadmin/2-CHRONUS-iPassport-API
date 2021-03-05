namespace iPassport.Api.Models.Requests
{
    public class PlanCreateRequest
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
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

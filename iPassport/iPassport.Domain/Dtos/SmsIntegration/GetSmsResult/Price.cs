using Newtonsoft.Json;

namespace iPassport.Domain.Dtos.SmsIntegration.GetSmsResult
{
    /// <summary>
    /// Sent SMS price.
    /// </summary>
    public class Price
    {
        /// <summary>
        /// The currency in which the price is expressed.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
        /// <summary>
        /// Price per one SMS.
        /// </summary>
        [JsonProperty("pricePerMessage")]
        public decimal? PricePerMessage { get; set; }
    }
}

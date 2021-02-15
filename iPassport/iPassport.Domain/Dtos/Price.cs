using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPassport.Domain.Dtos
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

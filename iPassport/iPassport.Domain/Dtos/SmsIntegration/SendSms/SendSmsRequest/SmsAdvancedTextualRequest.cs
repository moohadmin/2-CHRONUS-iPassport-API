using Newtonsoft.Json;
using System.Collections.Generic;

namespace iPassport.Domain.Dtos.SmsIntegration.SendSms.SendSmsRequest
{
    public class SmsAdvancedTextualRequest
    {
        /// <summary>
        /// Tracking information
        /// </summary>
        [JsonProperty("tracking")]
        public Tracking Tracking { get; set; }

        /// <summary>
        /// List of messages
        /// </summary>
        [JsonProperty("messages")]
        public IList<Message> Messages { get; set; }

        /// <summary>
        /// The ID which uniquely identifies the request. Bulk ID will be received only when you 
        /// send a message to more than one destination address.
        /// </summary>
        [JsonProperty("bulkId")]
        public string BulkId { get; set; }
    }
}

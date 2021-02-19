using Newtonsoft.Json;
using System.Collections.Generic;

namespace iPassport.Domain.Dtos.SmsIntegration.SendSms.SendSmsResponse
{
    public class SmsSendReponse
    {
        /// <summary>
        /// The ID that uniquely identifies the request. Bulk ID will be received only when you send 
        /// a message to more than one destination address.
        /// </summary>
        [JsonProperty("bulkId")]
        public string BulkId { get; set; }

        /// <summary>
        /// Array of sent message objects, one object per every message.
        /// </summary>
        [JsonProperty("messages")]
        public IList<SmsResponseDetails> Messages { get; set; }
    }
}

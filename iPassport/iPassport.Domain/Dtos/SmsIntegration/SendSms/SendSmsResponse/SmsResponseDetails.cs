using Newtonsoft.Json;

namespace iPassport.Domain.Dtos.SmsIntegration.SendSms.SendSmsResponse
{
    public class SmsResponseDetails
    {
        /// <summary>
        /// Indicates whether the message is successfully sent, not sent, delivered, not delivered, 
        /// waiting for delivery or any other possible status.
        /// </summary>
        [JsonProperty("status")]
        public Status Status { get; set; }
        /// <summary>
        /// The ID that uniquely identifies the message sent.
        /// </summary>
        [JsonProperty("messageId")]
        public string MessageId { get; set; }
        /// <summary>
        /// The message destination address
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }
    }
}

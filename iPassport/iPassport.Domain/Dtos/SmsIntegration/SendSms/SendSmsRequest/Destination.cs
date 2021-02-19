using Newtonsoft.Json;

namespace iPassport.Domain.Dtos.SmsIntegration.SendSms.SendSmsRequest
{
    public class Destination
    {
        /// <summary>
        /// required
        /// Message destination address. Addresses must be in international format (Example: 41793026727).
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }
        /// <summary>
        /// The ID that uniquely identifies the message sent.
        /// </summary>
        [JsonProperty("messageId")]
        public string MessageId { get; set; }
    }
}

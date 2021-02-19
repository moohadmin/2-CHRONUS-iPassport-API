using Newtonsoft.Json;

namespace iPassport.Domain.Dtos.SmsIntegration.SendSms.SendSmsRequest
{
    public class Tracking
    {
        /// <summary>
        /// Indicates if the message has to be tracked for Conversion rates. Possible values: SMS and URL
        /// </summary>
        [JsonProperty("track")]
        public string Track { get; set; }
        /// <summary>
        /// Key that uniquely identifies Conversion tracking process.
        /// </summary>
        [JsonProperty("processKey")]
        public string ProcessKey { get; set; }
        /// <summary>
        /// User-defined type of the Conversion tracking process or flow type or message type, 
        /// etc. Example: ONE_TIME_PIN or SOCIAL_INVITES.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// Custom base url used for shortening links from SMS text in URL Conversion rate tracking use-case.
        /// </summary>
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }
    }
}

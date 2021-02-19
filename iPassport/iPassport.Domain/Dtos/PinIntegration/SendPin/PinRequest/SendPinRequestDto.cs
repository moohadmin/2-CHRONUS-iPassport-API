using Newtonsoft.Json;

namespace iPassport.Domain.Dtos.PinIntegration.SendPin.PinRequest
{
    public class SendPinRequestDto
    {
        /// <summary>
        /// List of messages
        /// </summary>
        [JsonProperty("messages")]
        public MessageDto Messages { get; set; }
        
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace iPassport.Domain.Dtos.PinIntegration.SendPin.PinRequest
{
    
    public class MessageDto
    {
        /// <summary>
        /// Represents a sender ID which can be alphanumeric or numeric. Alphanumeric sender ID length should be
        /// between 3 and 11 characters (Example: CompanyName). Numeric sender ID length should be between 3 and 14 characters.
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }
        /// <summary>
        /// List of Destinations
        /// </summary>
        [JsonProperty("destinations")]
        public IList<DestinationDto> Destinations { get; set; }
        /// <summary>
        /// Text of the message that will be sent.
        /// </summary>
        [JsonProperty("text")]
        public string Text{get; set;}
    }
}

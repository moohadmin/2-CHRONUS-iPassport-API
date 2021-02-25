using System.Collections.Generic;

namespace iPassport.Domain.Dtos.PinIntegration.SendPin.PinResponse
{
    public class SendPinResponseDto
    {
        /// <summary>
        /// The ID that uniquely identifies the request. Bulk ID will be received only when you send 
        /// a message to more than one destination address.
        /// </summary>
        public string BulkId { get; set; }

        /// <summary>
        /// Array of sent message objects, one object per every message.
        /// </summary>
        public IList<SendPindResponseDetailsDto> Messages { get; set; }
    }
}

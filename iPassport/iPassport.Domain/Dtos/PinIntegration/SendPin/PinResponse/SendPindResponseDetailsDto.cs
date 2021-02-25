namespace iPassport.Domain.Dtos.PinIntegration.SendPin.PinResponse
{
    public class SendPindResponseDetailsDto
    {
        /// <summary>
        /// Indicates whether the message is successfully sent, not sent, delivered, not delivered, 
        /// waiting for delivery or any other possible status.
        /// </summary>
        public StatusDto Status { get; set; }
        /// <summary>
        /// The ID that uniquely identifies the message sent.
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// The message destination address
        /// </summary>
        public string To { get; set; }
    }
}

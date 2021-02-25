namespace iPassport.Domain.Dtos.PinIntegration.SendPin.PinRequest
{
    public class DestinationDto
    {
        /// <summary>
        /// required
        /// Message destination address. Addresses must be in international format (Example: 41793026727).
        /// </summary>
        public string To { get; set; }
    }
}

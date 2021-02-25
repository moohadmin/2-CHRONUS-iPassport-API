namespace iPassport.Domain.Dtos.PinIntegration
{
    /// <summary>
    /// Indicates whether the message is successfully sent, not sent, delivered, not delivered, waiting for delivery or any other possible status.
    /// </summary>
    public class StatusDto
    {
        /// <summary>
        /// Status ID.
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Status group name.
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// Human-readable description of the status.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Action that should be taken to eliminate the error.
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// Status name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// tatus group ID.
        /// </summary>
        public int? GroupId { get; set; }
    }
}

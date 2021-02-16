using Newtonsoft.Json;


namespace iPassport.Domain.Dtos.SmsIntegration
{
    /// <summary>
    /// Indicates whether the message is successfully sent, not sent, delivered, not delivered, waiting for delivery or any other possible status.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Status ID.
        /// </summary>
        [JsonProperty("id")]
        public int? Id { get; set; }
        /// <summary>
        /// Status group name.
        /// </summary>
        [JsonProperty("groupName")]
        public string GroupName { get; set; }
        /// <summary>
        /// Human-readable description of the status.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// Action that should be taken to eliminate the error.
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }
        /// <summary>
        /// Status name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// tatus group ID.
        /// </summary>
        [JsonProperty("groupId")]
        public int? GroupId { get; set; }
    }
}

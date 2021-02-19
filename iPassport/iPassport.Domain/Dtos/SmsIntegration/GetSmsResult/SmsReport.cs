using Newtonsoft.Json;
using System;

namespace iPassport.Domain.Dtos.SmsIntegration.GetSmsResult
{
    public class SmsReport
    {
        /// <summary>
        /// The number of parts the sent SMS was split into.
        /// </summary>
        [JsonProperty("smsCount")]
        public int? SmsCount { get; set; }
        
        /// <summary>
        /// Mobile country and network codes.
        /// </summary>
        [JsonProperty("mccMnc")]
        public string MccMnc { get; set; }
        
        /// <summary>
        /// Bulk ID.
        /// </summary>
        [JsonProperty("bulkId")]
        public string BulkId { get; set; }

        /// <summary>
        /// Indicates whether the error occurred during the query execution.
        /// </summary>
        [JsonProperty("error")]
        public Error Error { get; set; }
        
        /// <summary>
        /// Destination address.
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// Tells when the SMS was sent. Has the following format: yyyy-MM-dd'T'HH:mm:ss.SSSZ
        /// </summary>
        [JsonProperty("sentAt")]
        public DateTime SentAt { get; set; } //
        /// <summary>
        /// Message ID.
        /// </summary>
        [JsonProperty("messageId")]
        public string MessageId { get; set; }
        /// <summary>
        /// Tells when the SMS was finished processing by Infobip (i.e., delivered to the destination, delivered to the destination network, etc.). Has the following format: yyyy-MM-dd'T'HH:mm:ss.SSSZ.
        /// </summary>
        [JsonProperty("doneAt")]
        public DateTime DoneAt { get; set; }
        /// <summary>
        /// Indicates whether the message is successfully sent, not sent, delivered, not delivered, waiting for delivery or any other possible status.
        /// </summary>
        [JsonProperty("status")]
        public Status Status { get; set; }
        /// <summary>
        /// Sender ID that can be alphanumeric or numeric.
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }
        /// <summary>
        /// Callback data sent through callbackData field in fully featured SMS message.
        /// </summary>
        [JsonProperty("callbackData")]
        public string CallbackData { get; set; }

        /// <summary>
        /// Sent SMS price.
        /// </summary>
        [JsonProperty("price")]
        public Price Price { get; set; }
    }

}

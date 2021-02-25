using System;

namespace iPassport.Domain.Dtos.PinIntegration.FindPin
{
    public class PinReportResponseDetailsDto
    {
        /// <summary>
        /// The number of parts the sent SMS was split into.
        /// </summary>
        public int? SmsCount { get; set; }

        /// <summary>
        /// Mobile country and network codes.
        /// </summary>
        public string MccMnc { get; set; }

        /// <summary>
        /// Bulk ID.
        /// </summary>
        public string BulkId { get; set; }

        /// <summary>
        /// Indicates whether the error occurred during the query execution.
        /// </summary>
        public ErrorDto Error { get; set; }

        /// <summary>
        /// Destination address.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Tells when the SMS was sent. Has the following format: yyyy-MM-dd'T'HH:mm:ss.SSSZ
        /// </summary>
        public DateTime SentAt { get; set; } //
        /// <summary>
        /// Message ID.
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// Tells when the SMS was finished processing by Infobip (i.e., delivered to the destination, delivered to the destination network, etc.). Has the following format: yyyy-MM-dd'T'HH:mm:ss.SSSZ.
        /// </summary>
        public DateTime DoneAt { get; set; }
        /// <summary>
        /// Indicates whether the message is successfully sent, not sent, delivered, not delivered, waiting for delivery or any other possible status.
        /// </summary>
        public StatusDto Status { get; set; }
        /// <summary>
        /// Sender ID that can be alphanumeric or numeric.
        /// </summary>
        public string From { get; set; }

    }

}

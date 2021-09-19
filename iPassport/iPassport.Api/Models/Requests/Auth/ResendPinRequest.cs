using System;

namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Resend Pin Request model
    /// </summary>
    public class ResendPinRequest
    {
        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// User Id
        /// </summary>
        public Guid UserId { get; set; }
    }
}
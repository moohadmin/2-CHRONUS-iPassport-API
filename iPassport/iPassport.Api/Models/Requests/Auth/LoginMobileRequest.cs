using System;

namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Login Mobile Request model
    /// </summary>
    public class LoginMobileRequest
    {
        /// <summary>
        /// Pin
        /// </summary>
        public int Pin { get; set; }
        /// <summary>
        /// User Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Accept Terms
        /// </summary>
        public bool AcceptTerms { get; set; }
    }
}
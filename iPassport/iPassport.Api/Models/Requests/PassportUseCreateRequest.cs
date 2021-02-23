using System;

namespace iPassport.Api.Models.Requests
{
    public class PassportUseCreateRequest
    {
        /// <summary>
        /// PassportDetails Id
        /// </summary>
        public Guid PassportDetailsId { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public string Longitude { get; set; }
    }
}

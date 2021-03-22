using System;

namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// AddressEditRequest Class
    /// </summary>
    public class AddressEditRequest
    {
        /// <summary>
        /// AddressId
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Cep
        /// </summary>
        public string Cep { get; set; }
        /// <summary>
        /// Number
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// District
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// CityId
        /// </summary>
        public Guid CityId { get; set; }
    }
}

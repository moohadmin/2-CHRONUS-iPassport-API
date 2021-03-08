using System;

namespace iPassport.Api.Models.Requests
{
    public class AddressCreateRequest
    {
        /// <summary>
        /// Address
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string Cep { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public Guid CityId { get; set; }
    }
}

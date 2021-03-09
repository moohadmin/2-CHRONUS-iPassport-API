using System;
using System.ComponentModel.DataAnnotations;

namespace iPassport.Api.Models.Requests
{
    public class AddressCreateRequest
    {
        /// <summary>
        /// Address
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = true)]
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

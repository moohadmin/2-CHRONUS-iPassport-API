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
        public System.Guid CityId { get; set; }
        
    }
}

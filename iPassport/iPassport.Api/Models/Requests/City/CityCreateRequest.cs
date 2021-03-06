namespace iPassport.Api.Models.Requests
{
    public class CityCreateRequest
    {
        /// <summary>
        /// City Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// City Ibge COde
        /// </summary>
        public int IbgeCode { get; set; }
        /// <summary>
        /// City Population
        /// </summary>
        public long? Population { get; set; }
        /// <summary>
        /// City's State ID
        /// </summary>
        public System.Guid StateId { get; set; }
    }
}

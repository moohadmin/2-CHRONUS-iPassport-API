namespace iPassport.Api.Models.Requests
{
    public class CountryCreateRequest
    {
        /// <summary>
        /// Country Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Country Acronym
        /// </summary>
        public string Acronym { get; set; }
        /// <summary>
        /// Country External Code Iso
        /// </summary>
        public string ExternalCode { get; set; }
        /// <summary>
        /// Population
        /// </summary>
        public long? Population { get; set; }
    }
}

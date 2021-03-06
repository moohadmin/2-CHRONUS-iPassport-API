namespace iPassport.Api.Models.Requests
{
    public class StateCreateRequest
    {
        /// <summary>
        /// State Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// State Acronym
        /// </summary>
        public string Acronym { get; set; }
        /// <summary>
        /// State Ibge COde
        /// </summary>
        public int IbgeCode { get; set; }
        /// <summary>
        /// State Population
        /// </summary>
        public long? Population { get; set; }
        /// <summary>
        /// State's country ID
        /// </summary>
        public System.Guid CountryId { get; set; }
    }
}

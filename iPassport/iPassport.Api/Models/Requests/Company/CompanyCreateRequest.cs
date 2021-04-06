namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Company Create Request model
    /// </summary>
    public class CompanyCreateRequest
    {
        /// <summary>
        /// Company Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Company Cnpj
        /// </summary>
        public string Cnpj { get; set; }
        /// <summary>
        /// Company Address
        /// </summary>
        public AddressCreateRequest Address { get; set; }
        
    }
}

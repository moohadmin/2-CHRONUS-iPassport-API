namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Create User Agent Request
    /// </summary>
    public class UserAgentCreateRequest
    {
        public string FullName { get; set; }
        /// <summary>
        /// Vendo se aparece
        /// </summary>
        public string CPF { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public AddressCreateRequest Address { get; set; }
        public System.Guid CompanyId { get; set; }
        public bool PasswordIsValid { get; set; }
        
    }
}

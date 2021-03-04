namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Create User Request
    /// </summary>
    public class UserCreateRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool PasswordIsValid { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int Profile { get; set; }
        public string FullName { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string CNS { get; set; }
        public string Passport { get; set; }
        public System.DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Breed { get; set; }
        public string BloodType { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        /// <summary>
        /// Cod. de Identificação
        /// </summary>
        public string InternationalDocument { get; set; }
    }
}

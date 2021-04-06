namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Email Login Request model 
    /// </summary>
    public class EmailLoginRequest
    {
        /// <summary>
        /// Nome de usu�rio
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha do usu�rio
        /// </summary>
        public string Password { get; set; }
    }
}
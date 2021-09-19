namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Basic Login Request model
    /// </summary>
    public class BasicLoginRequest
    {
        /// <summary>
        /// Nome de usuário
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Password { get; set; }
    }
}
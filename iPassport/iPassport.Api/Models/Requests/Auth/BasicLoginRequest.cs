namespace iPassport.Api.Models.Requests
{
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
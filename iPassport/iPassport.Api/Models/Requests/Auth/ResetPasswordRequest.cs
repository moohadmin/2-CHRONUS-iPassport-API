namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Reset Password Request class
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Password Confirm
        /// </summary>
        public string PasswordConfirm { get; set; }
    }
}

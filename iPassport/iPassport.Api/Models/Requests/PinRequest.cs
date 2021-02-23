namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Send Pin model
    /// </summary>
    public class PinRequest
    {
        /// <summary>
        /// User phone number Ex.5527991920304
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Types documents (CPF, CNS, RG, Passport)
        /// </summary>
        public string doctype { get; set; }
        /// <summary>
        /// Document Number
        /// </summary>
        public string document { get; set; }
    }
}

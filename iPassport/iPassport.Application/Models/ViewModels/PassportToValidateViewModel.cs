using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Passport View Model
    /// </summary>
    public class PassportToValidateViewModel
    {
        /// <summary>
        /// Passport Details Id
        /// </summary>
        public Guid PassportDetailsId { get; set; }
        /// <summary>
        /// PassId
        /// </summary>
        public string PassId { get; set; }
        /// <summary>
        /// User Fullname 
        /// </summary>
        public string UserFullName { get; set; }
        /// <summary>
        /// Expiration Date
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// Identificação CPF
        /// </summary>
        public string Cpf { get; set; }
        /// <summary>
        /// User Photo
        /// </summary>
        public string UserPhoto { get; set; }
        /// <summary>
        /// if Imunized
        /// </summary>
        public bool Immunized { get; set; }
    }
}

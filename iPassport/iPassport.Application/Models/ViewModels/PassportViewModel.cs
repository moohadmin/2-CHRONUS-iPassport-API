using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Passport View Model
    /// </summary>
    public class PassportViewModel
    {
        /// <summary>
        /// PassportDetailsId
        /// </summary>
        public Guid PassportDetailsId { get; set; }
        /// <summary>
        /// PassId
        /// </summary>
        public string PassId { get; set; }
        /// <summary>
        /// User Fullname 
        /// </summary>
        public string UserFullname { get; set; }
        /// <summary>
        /// ExpirationDate
        /// </summary>
        public DateTime ExpirationDate { get; set; }

    }
}

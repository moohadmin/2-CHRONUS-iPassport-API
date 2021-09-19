using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Company Create Response ViewModel
    /// </summary>
    public class CompanyCreateResponseViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Company can associate 
        /// </summary>
        public bool CanAssociate { get; set; }
    }
}

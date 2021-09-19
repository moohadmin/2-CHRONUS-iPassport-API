using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// CompanyType ViewModel
    /// </summary>
    public class CompanyTypeViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Company Type Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Company Identifyer code
        /// </summary>
        public int Identifyer { get; set; }

    }
}

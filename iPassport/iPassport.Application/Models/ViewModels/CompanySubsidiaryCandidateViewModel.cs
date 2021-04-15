using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Company Subsidiary Candidate View Model
    /// </summary>
    public class CompanySubsidiaryCandidateViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Company's Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Company CNPJ
        /// </summary>
        public string Cnpj { get; set; }        
        /// <summary>
        /// Company Segment
        /// </summary>
        public string Segment { get; set; }
        
    }
}

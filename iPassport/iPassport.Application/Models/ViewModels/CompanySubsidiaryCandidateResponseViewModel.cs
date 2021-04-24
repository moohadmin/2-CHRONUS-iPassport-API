using System;
using System.Collections.Generic;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Company Subsidiary Candidate View Model
    /// </summary>
    public class CompanySubsidiaryCandidateResponseViewModel
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Company's Name
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Segment Name
        /// </summary>
        public string SegmentName { get; set; }        
        /// <summary>
        /// Company Candidates
        /// </summary>
        public IList<CompanySubsidiaryCandidateViewModel> Candidates { get; set; }
        
    }
}

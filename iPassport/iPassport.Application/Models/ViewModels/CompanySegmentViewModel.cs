using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Segment View Model
    /// </summary>
    public class CompanySegmentViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Segment's Names
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Segment's Identifyer
        /// </summary>
        public int Identifyer { get; set; }
        /// <summary>
        /// Company Type
        /// </summary>
        public CompanyTypeViewModel CompanyType { get; set; }
        
    }
}

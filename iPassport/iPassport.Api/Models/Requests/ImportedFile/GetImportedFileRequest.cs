using System;

namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// GetImportedFileRequest Class
    /// </summary>
    public class GetImportedFileRequest : PageFilterRequest
    {
        /// <summary>
        /// Start Time property
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// End Time property
        /// </summary>
        public DateTime? EndTime { get; set; }
        
    }
}

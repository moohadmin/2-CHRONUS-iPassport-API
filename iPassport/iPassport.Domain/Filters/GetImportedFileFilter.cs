using System;

namespace iPassport.Domain.Filters
{
    public class GetImportedFileFilter : PageFilter
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }        
    }
}

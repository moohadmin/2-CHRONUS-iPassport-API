using iPassport.Domain.Enums;
using System;

namespace iPassport.Domain.Filters
{
    public class GetRegisteredUserCountFilter
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }        
        public EProfileType Profile { get; set; }
    }
}

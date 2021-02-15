using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPassport.Domain.Dtos
{
    public class SmsReportResponse
    {
        /// <summary>
        /// Array Of Objects retorned
        /// </summary>
        [JsonProperty("results")]
        public IList<SmsReport> Results { get; set; }
    }
}

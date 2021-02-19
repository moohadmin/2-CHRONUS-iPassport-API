using Newtonsoft.Json;
using System.Collections.Generic;

namespace iPassport.Domain.Dtos.SmsIntegration.GetSmsResult
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

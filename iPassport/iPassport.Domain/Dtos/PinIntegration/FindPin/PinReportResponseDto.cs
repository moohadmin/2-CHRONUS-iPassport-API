using Newtonsoft.Json;
using System.Collections.Generic;

namespace iPassport.Domain.Dtos.PinIntegration.FindPin
{
    public class PinReportResponseDto
    {
        /// <summary>
        /// Array Of Objects retorned
        /// </summary>
        [JsonProperty("results")]
        public IList<PinReportResponseDetailsDto> Results { get; set; }
    }
}

using System.Collections.Generic;

namespace iPassport.Domain.Dtos.PinIntegration.FindPin
{
    public class PinReportResponseDto
    {
        /// <summary>
        /// Array Of Objects retorned
        /// </summary>
        public IList<PinReportResponseDetailsDto> Results { get; set; }
    }
}

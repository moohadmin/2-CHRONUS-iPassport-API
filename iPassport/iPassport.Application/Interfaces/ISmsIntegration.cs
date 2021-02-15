using iPassport.Domain.Dtos;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace iPassport.Application.Interfaces
{
    public interface ISmsIntegration
    {
        [Post("/sms/2/text/advanced")]
        Task<SmsReportResponse> SendSmsMessage();

        [Get("/sms/1/reports")]
        Task<SmsReportResponse> GetSmsResult();
    }
}

using iPassport.Application.Interfaces;
using iPassport.Domain.Dtos.SmsIntegration.GetSmsResult;
using iPassport.Domain.Dtos.SmsIntegration.SendSms.SendSmsRequest;
using iPassport.Domain.Dtos.SmsIntegration.SendSms.SendSmsResponse;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.ExternalServices
{
    public class SmsIntegration : ISmsExternalService
    {

        private readonly IConfiguration _config;
        public SmsIntegration(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Finds data from the sent message
        /// </summary>
        /// <param name="idMessage">Message id to search</param>
        /// <returns></returns>
        public Task<SmsReportResponse> GetSmsResult(string idMessage)
        {
            var client = new RestClient(GetSmsResultUrl());

            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", GetAuthorizationKey());
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            if (!String.IsNullOrWhiteSpace(idMessage))
            {
                request.AddQueryParameter("messageId", idMessage);
            }

            IRestResponse response = client.Execute(request);
            var ResponseContent = JsonConvert.DeserializeObject<SmsReportResponse>(response.Content);

            return Task.FromResult(ResponseContent);
        }

        /// <summary>
        /// Envia mensagem SMS
        /// </summary>
        /// <param name="smsAdvancedTextualRequest">Dto with the data for sending the SMS message</param>
        /// <returns></returns>
        public Task<SmsSendReponse> SendSmsMessage(SmsAdvancedTextualRequest smsAdvancedTextualRequest)
        {
            var client = new RestClient(GetSendMessageUrl());
            smsAdvancedTextualRequest.Messages.First().From = GetSmsFromNumber();

            var requestBody = JsonConvert.SerializeObject(smsAdvancedTextualRequest, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", GetAuthorizationKey());
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json",
                            requestBody,
                            ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            var ResponseContent = JsonConvert.DeserializeObject<SmsSendReponse>(response.Content);
            
            return Task.FromResult(ResponseContent);
        }
        /// <summary>
        /// Get Authorization Key
        /// </summary>
        /// <returns></returns>
        private string GetAuthorizationKey()
        {
            return _config.GetSection("SmsIntegration").GetSection("AuthenticationKey").Value;
        }
        /// <summary>
        /// Get service API Base Url
        /// </summary>
        /// <returns></returns>
        private string GetServiceBaseUrl()
        {
            return _config.GetSection("SmsIntegration").GetSection("BaseUrl").Value;
        }
        /// <summary>
        /// Get Send Url
        /// </summary>
        /// <returns></returns>
        private string GetSendMessageUrl()
        {
            return String.Concat(GetServiceBaseUrl(), _config.GetSection("SmsIntegration").GetSection("SendApiUrl").Value);
        }
        /// <summary>
        /// Get sms result Url
        /// </summary>
        /// <returns></returns>
        private string GetSmsResultUrl()
        {
            return String.Concat(GetServiceBaseUrl(), _config.GetSection("SmsIntegration").GetSection("GetApiUrl").Value);
        }
        /// <summary>
        /// Get From Number
        /// </summary>
        /// <returns></returns>
        private string GetSmsFromNumber()
        {
            return _config.GetSection("SmsIntegration").GetSection("FromNumber").Value;
        }
    }
}

using iPassport.Application.Exceptions;
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
        public Task<SmsReportResponse> FindPin(string idMessage)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("messageId", idMessage);

            var response = RunIntegration(Method.GET, GetFindMessageUrl(), ParameterType.QueryString, null, queryParams);
            var ResponseContent = JsonConvert.DeserializeObject<SmsReportResponse>(response.Content);

            return Task.FromResult(ResponseContent);
            
        }

        /// <summary>
        /// Envia mensagem SMS
        /// </summary>
        /// <param name="smsAdvancedTextualRequest">Dto with the data for sending the SMS message</param>
        /// <returns></returns>
        public Task<SmsSendReponse> SendPin(SmsAdvancedTextualRequest smsAdvancedTextualRequest)
        {            
            smsAdvancedTextualRequest.Messages.First().From = GetSmsFromNumber();
            var requestBody = JsonConvert.SerializeObject(smsAdvancedTextualRequest, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var response = RunIntegration(Method.POST, GetSendMessageUrl(), ParameterType.RequestBody, requestBody, null);            
            var ResponseContent = JsonConvert.DeserializeObject<SmsSendReponse>(response.Content);
            
            return Task.FromResult(ResponseContent);
        }

        private IRestResponse RunIntegration(Method method, string ComunicationUrl, ParameterType parameterType, string requestBody, Dictionary<string, string> queryParams)
        {
            var client = new RestClient(ComunicationUrl);
            client.Timeout = -1;
            var request = new RestRequest(method);
            request.AddHeader("Authorization", GetAuthorizationKey());
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            switch (parameterType)
            {                
                case ParameterType.RequestBody:
                    request.AddParameter("application/json",
                            requestBody,
                            ParameterType.RequestBody);
                    break;
                case ParameterType.QueryString:
                    foreach (KeyValuePair<string, string> entry in queryParams)
                    {
                        request.AddQueryParameter(entry.Key, entry.Value);
                    }
                    break;                
                default:
                    throw new Exception("Tipo de paramentro não permitido.");
            }

            IRestResponse response = client.Execute(request);

            return response;
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
        /// Get Send Url
        /// </summary>
        /// <returns></returns>
        private string GetSendMessageUrl()
        {
            return String.Concat(_config.GetSection("SmsIntegration").GetSection("BaseUrl").Value, _config.GetSection("SmsIntegration").GetSection("SendApiUrl").Value);
        
        }
        /// <summary>
        /// Get sms result Url
        /// </summary>
        /// <returns></returns>
        private string GetFindMessageUrl()
        {
            return String.Concat(_config.GetSection("SmsIntegration").GetSection("BaseUrl").Value, _config.GetSection("SmsIntegration").GetSection("GetApiUrl").Value);
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

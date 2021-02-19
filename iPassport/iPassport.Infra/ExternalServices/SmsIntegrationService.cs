using iPassport.Application.Interfaces;
using iPassport.Domain.Dtos.PinIntegration;
using iPassport.Domain.Dtos.PinIntegration.FindPin;
using iPassport.Domain.Dtos.PinIntegration.SendPin.PinRequest;
using iPassport.Domain.Dtos.PinIntegration.SendPin.PinResponse;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Infra.ExternalServices
{
    public class SmsIntegrationService : ISmsExternalService
    {
        private readonly IConfiguration _config;

        public SmsIntegrationService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Finds data from the sent message
        /// </summary>
        /// <param name="idMessage">Message id to search</param>
        /// <returns></returns>
        public Task<PinReportResponseDto> FindPinSent(string idMessage)
        {
            PinReportResponseDto simulatedFindResponse = PinIntegrationSimulatedFind();
            if (simulatedFindResponse != null)
                return Task.FromResult(simulatedFindResponse);

            Dictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "messageId", idMessage }
            };

            var response = RunIntegration(Method.GET, GetFindMessageUrl(), ParameterType.QueryString, queryParams);
            var ResponseContent = JsonConvert.DeserializeObject<PinReportResponseDto>(response.Content);

            return Task.FromResult(ResponseContent);
        }



        /// <summary>
        /// Envia mensagem SMS
        /// </summary>
        /// <param name="sendPinRequestDto">Dto with the data for sending the SMS message</param>
        /// <returns></returns>
        public Task<SendPinResponseDto> SendPin(SendPinRequestDto sendPinRequestDto)
        {
            SendPinResponseDto simulatedSentResponse = PinIntegrationSimulatedSent();
            if (simulatedSentResponse != null)
                return Task.FromResult(simulatedSentResponse);

            sendPinRequestDto.Messages.From = GetSmsFromNumber();
            var requestBody = JsonConvert.SerializeObject(sendPinRequestDto, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            Dictionary<string, string> integrationParams = new Dictionary<string, string>
            {
                { "RequestBody", requestBody }
            };

            var response = RunIntegration(Method.POST, GetSendMessageUrl(), ParameterType.RequestBody, integrationParams);
            var ResponseContent = JsonConvert.DeserializeObject<SendPinResponseDto>(response.Content);

            return Task.FromResult(ResponseContent);
        }

        /// <summary>
        /// Run Integration with external service and return response
        /// </summary>
        /// <param name="method">method used to comunication</param>
        /// <param name="ComunicationUrl">Endpoint url </param>
        /// <param name="parameterType">Type of parans </param>        
        /// <param name="integrationParams">integration Params</param>
        /// <returns></returns>
        private IRestResponse RunIntegration(Method method, string ComunicationUrl, ParameterType parameterType, Dictionary<string, string> integrationParams)
        {
            var client = new RestClient(ComunicationUrl)
            {
                Timeout = -1
            };

            var request = new RestRequest(method);
            request.AddHeader("Authorization", GetAuthorizationKey());
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            switch (parameterType)
            {
                case ParameterType.RequestBody:
                    request.AddParameter("application/json",
                            integrationParams.GetValueOrDefault("RequestBody"),
                            ParameterType.RequestBody);
                    integrationParams.GetValueOrDefault("RequestBody");
                    break;
                case ParameterType.QueryString:
                    foreach (KeyValuePair<string, string> entry in integrationParams)
                    {
                        request.AddQueryParameter(entry.Key, entry.Value);
                    }
                    break;
                default:
                    throw new Exception("Tipo de paramentro não permitido.");
            }

            IRestResponse response = client.Execute(request);

            if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 600)
                throw new Exception(response.Content);

            return response;
        }

        /// <summary>
        /// Get Authorization Key
        /// </summary>
        /// <returns></returns>
        private string GetAuthorizationKey() => _config.GetSection("SmsIntegration").GetSection("AuthenticationKey").Value;

        /// <summary>
        /// Get Send Url
        /// </summary>
        /// <returns></returns>
        private string GetSendMessageUrl() => String.Concat(_config.GetSection("SmsIntegration").GetSection("BaseUrl").Value, _config.GetSection("SmsIntegration").GetSection("SendApiUrl").Value);

        /// <summary>
        /// Get sms result Url
        /// </summary>
        /// <returns></returns>
        private string GetFindMessageUrl() => String.Concat(_config.GetSection("SmsIntegration").GetSection("BaseUrl").Value, _config.GetSection("SmsIntegration").GetSection("GetApiUrl").Value);

        /// <summary>
        /// Get From Number
        /// </summary>
        /// <returns></returns>
        private string GetSmsFromNumber() => _config.GetSection("SmsIntegration").GetSection("FromNumber").Value;


        /// <summary>
        /// Mock to Find Pin Response
        /// </summary>
        /// <returns></returns>
        private PinReportResponseDto PinIntegrationSimulatedFind()
        {
            var AmbienteSimulado = Environment.GetEnvironmentVariable("PIN_INTEGRATION_SIMULADO");
            if (!string.IsNullOrWhiteSpace(AmbienteSimulado) && Convert.ToBoolean(AmbienteSimulado))
            {
                return new PinReportResponseDto()
                {
                    Results = new List<PinReportResponseDetailsDto>()
                    {
                        new PinReportResponseDetailsDto()
                        {
                             MessageId = "123456789",
                             Status = new StatusDto()
                             {
                                GroupId= 3,
                                GroupName= "DELIVERED",
                                Id= 5,
                                Name= "DELIVERED_TO_HANDSET",
                                Description= "Message delivered to handset"
                             }
                        }
                    }
                };
            }

            return null;
        }

        /// <summary>
        /// Mock to Sent Pin Response
        /// </summary>
        /// <returns></returns>
        private SendPinResponseDto PinIntegrationSimulatedSent()
        {
            var AmbienteSimulado = Environment.GetEnvironmentVariable("PIN_INTEGRATION_SIMULADO");
            if (!string.IsNullOrWhiteSpace(AmbienteSimulado) && Convert.ToBoolean(AmbienteSimulado))
            {
                return new SendPinResponseDto()
                {
                    Messages = new List<SendPindResponseDetailsDto>()
                    {
                        new SendPindResponseDetailsDto()
                        {
                             MessageId = "123456789",
                             Status = new StatusDto()
                             {
                                GroupId= 3,
                                GroupName= "DELIVERED",
                                Id= 5,
                                Name= "DELIVERED_TO_HANDSET",
                                Description= "Message delivered to handset"
                             }
                        }
                    }
                };
            }

            return null;
        }


    }
}

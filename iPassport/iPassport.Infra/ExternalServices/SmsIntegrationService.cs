using iPassport.Application.Interfaces;
using iPassport.Application.Services.Constants;
using iPassport.Domain.Dtos.PinIntegration;
using iPassport.Domain.Dtos.PinIntegration.FindPin;
using iPassport.Domain.Dtos.PinIntegration.SendPin.PinRequest;
using iPassport.Domain.Dtos.PinIntegration.SendPin.PinResponse;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace iPassport.Infra.ExternalServices
{
    public class SmsIntegrationService : ISmsExternalService
    {

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
            var ResponseContent = JsonSerializer.Deserialize<PinReportResponseDto>(response.Content, new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return Task.FromResult(ResponseContent);
        }

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="text"> Text of SMS</param>
        /// <param name="phone"> Destination phone</param>
        /// <returns></returns>
        public Task<SendPinResponseDto> SendPin(string text, string phone)
        {
            SendPinResponseDto simulatedSentResponse = PinIntegrationSimulatedSent(phone);

            if (simulatedSentResponse != null)
                return Task.FromResult(simulatedSentResponse);

            var sendPinRequestDto = GenerateSendRequestDto(text, phone);

            var requestBody = JsonSerializer.Serialize(sendPinRequestDto, new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            Dictionary<string, string> integrationParams = new Dictionary<string, string>
            {
                { "RequestBody", requestBody }
            };

            var response = RunIntegration(Method.POST, GetSendMessageUrl(), ParameterType.RequestBody, integrationParams);
            var ResponseContent = JsonSerializer.Deserialize<SendPinResponseDto>(response.Content, new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

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

            var token = GetToken();

            var request = new RestRequest(method);
            //request.AddHeader("Authorization", GetAuthorizationKey());
            request.AddHeader("Authorization", $"Bearer {token}");
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

        private string GetToken()
        {
            var baseurl = EnvConstants.NOTIFICATIONS_BASE_URL;
            var model = new SmsTokenModel
            {
                clientId = EnvConstants.NOTIFICATIONS_CLIENT_ID,
                clientSecret = EnvConstants.NOTIFICATIONS_CLIENT_SECRET,
                grantType = EnvConstants.NOTIFICATIONS_GRANT_TYPE
            };

            var client = new RestClient($"{baseurl}/auth/1/oauth2/token") { Timeout = -1 };
            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", model.clientId);
            request.AddParameter("client_secret", model.clientSecret);
            request.AddParameter("grant_type", model.grantType);

            IRestResponse response = client.Execute(request);

            if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 600)
                throw new Exception(response.Content);

            var ResponseContent = JsonSerializer.Deserialize<SmsTokenResponseModel>(response.Content,new JsonSerializerOptions() 
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return ResponseContent.access_token;
        }

        /// <summary>
        /// Get Authorization Key
        /// </summary>
        /// <returns></returns>
        private string GetAuthorizationKey() => EnvConstants.NOTIFICATIONS_AUTHENTICATION_KEY;

        /// <summary>
        /// Get Send Url
        /// </summary>
        /// <returns></returns>
        private string GetSendMessageUrl() => String.Concat(EnvConstants.NOTIFICATIONS_BASE_URL, EnvConstants.NOTIFICATIONS_SEND_API_URL);

        /// <summary>
        /// Get sms result Url
        /// </summary>
        /// <returns></returns>
        private string GetFindMessageUrl() => String.Concat(EnvConstants.NOTIFICATIONS_BASE_URL, EnvConstants.NOTIFICATIONS_GET_API_URL);

        /// <summary>
        /// Get From Number
        /// </summary>
        /// <returns></returns>
        private string GetSmsFromNumber() => EnvConstants.NOTIFICATIONS_FROM_NUMBER;

        /// <summary>
        /// Mock to Find Pin Response
        /// </summary>
        /// <returns></returns>
        private PinReportResponseDto PinIntegrationSimulatedFind()
        {
            var AmbienteSimulado = EnvConstants.NOTIFICATIONS_MOCK;

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
        private SendPinResponseDto PinIntegrationSimulatedSent(string phone)
        {
            var IsNotificationMock = EnvConstants.NOTIFICATIONS_MOCK;
            var NotificationMockNumber = EnvConstants.NOTIFICATIONS_MOCK_NUMBER;

            if ((!string.IsNullOrWhiteSpace(IsNotificationMock) && Convert.ToBoolean(IsNotificationMock))
                || (!String.IsNullOrWhiteSpace(NotificationMockNumber) && NotificationMockNumber.Trim() == phone))
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

        private SendPinRequestDto GenerateSendRequestDto(string text, string phone)
        {
            return new SendPinRequestDto() { Messages = new MessageDto() { Text = text, From = GetSmsFromNumber(), Destinations = new List<DestinationDto>() { new DestinationDto() { To = phone } } } };
        }
    }
}

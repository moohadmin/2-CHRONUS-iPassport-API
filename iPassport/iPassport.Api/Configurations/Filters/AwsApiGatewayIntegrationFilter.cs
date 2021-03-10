using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace iPassport.Api.Configurations.Filters
{
    /// <summary>
    /// Assembly: Swashbuckle.AspNetCore.SwaggerGen.dll
    /// Class: Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    /// </summary>
    public class AwsApiGatewayIntegrationFilter : IOperationFilter
    {
        readonly string INTEGRATION_OBJECT_ATTRIBUTE = "x-amazon-apigateway-integration";

        /// <summary>
        /// Adds x-amazon-apigateway-integration attribute on swagger documment.
        ///
        /// Sample atribute:
        ///
        /// "x-amazon-apigateway-integration": {
        ///     "uri": "${uri_schema}://${uri_dns}/api/Auth",
        ///     "httpMethod": "POST",
        ///     "connectionType": "VPC_LINK",
        ///     "connectionId": "${connection_id}",
        ///     "type": "http_proxy",
        ///     "requestParameters": {
        ///          "integration.request.path.{parameter.Name}": "method.request.path.{parameter.Name}"
        ///          ...
        ///     }
        /// }
        /// </summary>
        /// <param name="operation">operation object</param>
        /// <param name="context">context object</param>
        /// <returns></returns>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            ApiDescription apiDescription = context.ApiDescription;

            if (apiDescription == null)
            {
                return;
            }

            string httpMethod = apiDescription.HttpMethod;
            string relativePath = apiDescription.RelativePath;

            if (string.IsNullOrWhiteSpace(httpMethod) || string.IsNullOrWhiteSpace(relativePath))
            {
                return;
            }

            string uri = "${LOAD_BALANCER_URN}";

            OpenApiObject integrationObject = new OpenApiObject {
                ["uri"] = new OpenApiString(uri),
                ["httpMethod"] = new OpenApiString("ANY"),
                ["connectionType"] = new OpenApiString("VPC_LINK"),
                ["connectionId"] = new OpenApiString("${CONNECTION_ID}"),
                ["type"] = new OpenApiString("http_proxy"),
                ["payloadFormatVersion"] = new OpenApiString("1.0"),
            };

            var parameters = apiDescription.ParameterDescriptions;

            OpenApiObject requestParameters = new OpenApiObject();

            foreach (var parameter in parameters)
            {
                if (BindingSource.Path ==  parameter.Source)
                {
                    requestParameters[$"integration.request.path.{parameter.Name}"] = new OpenApiString($"method.request.path.{parameter.Name}");
                }
            }

            if (requestParameters.Count > 0)
            {
                integrationObject["requestParameters"] = requestParameters;    
            }

            operation.Extensions.Add(INTEGRATION_OBJECT_ATTRIBUTE, integrationObject);
        }
    }
}
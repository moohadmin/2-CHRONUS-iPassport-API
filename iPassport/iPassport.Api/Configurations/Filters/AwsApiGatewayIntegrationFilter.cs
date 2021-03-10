using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

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
            AddApiGatewayPathParamIntegration(operation, context);
            AddApiGatewayCorsHeader(operation, context);
        }

        private void AddApiGatewayPathParamIntegration(OpenApiOperation operation, OperationFilterContext context)
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

            OpenApiObject integrationObject = new OpenApiObject
            {
                ["uri"] = new OpenApiString("${LOAD_BALANCER_URN}"),
                ["httpMethod"] = new OpenApiString(httpMethod),
                ["connectionType"] = new OpenApiString("VPC_LINK"),
                ["connectionId"] = new OpenApiString("${CONNECTION_ID}"),
                ["type"] = new OpenApiString("http_proxy"),
                ["payloadFormatVersion"] = new OpenApiString("1.0")
            };

            var parameters = apiDescription.ParameterDescriptions;

            OpenApiObject requestParameters = new OpenApiObject();

            foreach (var parameter in parameters)
            {
                if (BindingSource.Path == parameter.Source)
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

        private void AddApiGatewayCorsHeader(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.RequestBody != null && operation.RequestBody.Content.ContainsKey("multipart/form-data"))
            {
                operation.Parameters.Add(new OpenApiParameter { Name = "Content-Type", In = ParameterLocation.Header, Description = "Content-Type", Required = true });
                operation.Parameters.Add(new OpenApiParameter { Name = "Accept", In = ParameterLocation.Header, Description = "Accept", Required = true });
            }

            foreach (var resp in operation.Responses)
            {
                resp.Value.Headers.Add("Access-Control-Allow-Origin", new OpenApiHeader { Extensions = new Dictionary<string, IOpenApiExtension> { { "type", new OpenApiString("string") } } });
            }
        }
    }
}
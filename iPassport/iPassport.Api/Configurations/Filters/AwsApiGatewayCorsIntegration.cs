using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace iPassport.Api.Configurations.Filters
{
    public class AwsApiGatewayCorsIntegration : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var responses = GetResponses();

            foreach (var path in swaggerDoc.Paths)
            {
                path.Value.Operations.Add(OperationType.Options, new OpenApiOperation
                {
                    Extensions = GetExtensions(path.Value.Operations),
                    Responses = responses
                });
            }
        }

        private Dictionary<string, IOpenApiExtension> GetExtensions(IDictionary<OperationType, OpenApiOperation> operations)
        {
            string headerAllowMethodsValue = "'" + string.Join(',', operations.Keys.Select(x => x.ToString().ToUpper())) + ",OPTIONS'";

            OpenApiObject extentionsIntegrationObjectResponses = new OpenApiObject
            {
                ["default"] = new OpenApiObject
                {
                    ["statusCode"] = new OpenApiString("200"),
                    ["responseParameters"] = new OpenApiObject
                    {
                        ["method.response.header.Access-Control-Allow-Methods"] = new OpenApiString(headerAllowMethodsValue),
                        ["method.response.header.Access-Control-Allow-Headers"] = new OpenApiString("'Content-Type,X-Amz-Date,Authorization,X-Api-Key,X-Amz-Security-Token,Access-Token'"),
                        ["method.response.header.Access-Control-Allow-Origin"] = new OpenApiString("'*'")
                    }
                }
            };

            OpenApiObject extensionsIntegrationObject = new OpenApiObject()
            {
                ["responses"] = extentionsIntegrationObjectResponses,
                ["requestTemplates"] = new OpenApiObject() { ["application/json"] = new OpenApiString("{\"statusCode\": 200}") },
                ["passthroughBehavior"] = new OpenApiString("when_no_match"),
                ["type"] = new OpenApiString("mock")
            };

            OpenApiArray extensionsConsumesProducesValue = new OpenApiArray() { new OpenApiString("application/json") };

            return new Dictionary<string, IOpenApiExtension>
            {
                { "consumes", extensionsConsumesProducesValue },
                { "produces", extensionsConsumesProducesValue },
                { "x-amazon-apigateway-integration", extensionsIntegrationObject }
            };
        }

        private OpenApiResponses GetResponses()
        {
            var extensionTypeHeader = new Dictionary<string, IOpenApiExtension> { { "type", new OpenApiString("string") } };

            return new OpenApiResponses
            {
                { 
                    "200", new OpenApiResponse
                    {
                        Headers = new Dictionary<string, OpenApiHeader>()
                        {
                            { "Access-Control-Allow-Origin", new OpenApiHeader() { Extensions = extensionTypeHeader } },
                            { "Access-Control-Allow-Methods", new OpenApiHeader() { Extensions = extensionTypeHeader } },
                            { "Access-Control-Allow-Headers", new OpenApiHeader() { Extensions = extensionTypeHeader } }
                        },
                        Extensions = new Dictionary<string, IOpenApiExtension> { { "schema", new OpenApiObject() { ["$ref"] = new OpenApiString("#/definitions/Empty") } } }
                    }
                }
            };
        }
    }
}

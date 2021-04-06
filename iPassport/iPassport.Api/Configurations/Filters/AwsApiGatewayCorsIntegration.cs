using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace iPassport.Api.Configurations.Filters
{
    /// <summary>
    /// Aws Api Gateway Cors Integration class
    /// </summary>
    public class AwsApiGatewayCorsIntegration : IDocumentFilter
    {
        /// <summary>
        /// Apply Method
        /// </summary>
        /// <param name="swaggerDoc">Open Api Document with swagger documentation</param>
        /// <param name="context">Document Filter Context</param>
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

            swaggerDoc.Paths.Add("/swagger/{proxy+}", new OpenApiPathItem()
            {
                Extensions = new Dictionary<string, IOpenApiExtension>()
                {
                    {
                        "x-amazon-apigateway-any-method",
                        new OpenApiObject()
                        {
                            ["responses"] = new OpenApiObject()
                            {
                                ["default"] = new OpenApiObject()
                                {
                                    ["description"] = new OpenApiString("Default response for ANY /swagger/{proxy+}")
                                }
                            },
                            ["x-amazon-apigateway-integration"] = new OpenApiObject()
                            {
                                ["payloadFormatVersion"] = new OpenApiString("1.0"),
                                ["connectionId"] = new OpenApiString("${CONNECTION_ID}"),
                                ["type"] = new OpenApiString("http_proxy"),
                                ["httpMethod"] = new OpenApiString("ANY"),
                                ["uri"] = new OpenApiString("${LOAD_BALANCER_URN}"),
                                ["connectionType"] = new OpenApiString("VPC_LINK")
                            }
                        }
                    }
                }
            });
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
                ["type"] = new OpenApiString("mock"),
                ["uri"] = new OpenApiString("${LOAD_BALANCER_URN}"),
                ["httpMethod"] = new OpenApiString("ANY"),
                ["connectionType"] = new OpenApiString("VPC_LINK"),
                ["connectionId"] = new OpenApiString("${CONNECTION_ID}"),
                ["type"] = new OpenApiString("http_proxy"),
                ["payloadFormatVersion"] = new OpenApiString("1.0")
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

    /// <summary>
    /// Custom Open Api Path Item Class
    /// </summary>
    public class CustomOpenApiPathItem : OpenApiPathItem
    {
        /// <summary>
        /// Operations Dictionary
        /// </summary>
        public new Dictionary<string, OpenApiOperation> Operations { get; set; }

        /// <summary>
        /// Open Api Responses
        /// </summary>
        public OpenApiResponses Responses { get; set; }
    }
}

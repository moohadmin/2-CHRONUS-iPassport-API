using System;
using System.Reflection;
using System.Text.Json;
using iPassport.Application.Exceptions;

namespace iPassport.Application.Services.Constants
{
    public static class EnvConstants
    {
        private readonly static string AWS_SECRET_DATABASE = "AWS_SECRET_DATABASE";
        private readonly static string AWS_SECRET_NOTIFICATIONS = "AWS_SECRET_NOTIFICATIONS";
        private readonly static string AWS_SECRET_JWT_TOKEN = "AWS_SECRET_JWT_TOKEN";
        private readonly static string AWS_SECRET_S3 = "AWS_SECRET_S3";

        public static string GetEnvironmentVariable(string name, string secretPayloadName, Func<JsonDocument, string> parser, bool errorIfEmpty = false)
        {
            var envValue = Environment.GetEnvironmentVariable(name);

            if (!string.IsNullOrWhiteSpace(envValue))
            {
                return envValue;
            }

            string secret = Environment.GetEnvironmentVariable(secretPayloadName);

            string value = null;

            if (!string.IsNullOrWhiteSpace(secret))
            {
                JsonDocument secretDocumment = JsonDocument.Parse(secret);
                value = parser(secretDocumment);
            }

            if (string.IsNullOrWhiteSpace(value) && errorIfEmpty)
            {
                throw new BusinessException($"Environment variable SecretPayloadName: {secretPayloadName} must contain value.");
            }

            return value;

        }

        public static string GetEnvironmentVariable(string name, string secretPayloadName, string payloadPath, bool errorIfEmpty = false)
        {
            return GetEnvironmentVariable(name, secretPayloadName, (jsonDocument) => {

                var pathResult = jsonDocument.SelectElement(payloadPath, false);

                string pathDescription = $"{secretPayloadName}[{payloadPath}]";

                if (pathResult == null)
                {
                    throw new BusinessException($"There is no path {pathDescription}");
                }

                return pathResult.Value.GetString();

            }, errorIfEmpty);

        }

        public static string GetEnvironmentVariable(string name)
        {
            return GetEnvironmentVariable(name, string.Empty, string.Empty, false);
        }

        public static string GetEnvironmentVariable(string name, string defaultValue)
        {
            string value = GetEnvironmentVariable(name);

            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            return value;

        }

        public static string GetRequiredEnvironmentVariable(string name)
        {
            return GetEnvironmentVariable(name, string.Empty, string.Empty, true);
        }

        public static readonly string PROJECT_NAME = Assembly.GetEntryAssembly().GetName().Name;
        public static readonly string ENVIRONMENT_NAME = GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        // Database
        public static readonly string DATABASE_CONNECTION_STRING = GetEnvironmentVariable("DATABASE_CONNECTION_STRING", AWS_SECRET_DATABASE, (jsonDocument) => {
            string username = jsonDocument.SelectElement("$.username").Value.GetString();
            string password = jsonDocument.SelectElement("$.password").Value.GetString();
            // string engine = jsonDocument.SelectElement("$.engine").Value.GetString();
            string host = jsonDocument.SelectElement("$.host").Value.GetString();
            long port = jsonDocument.SelectElement("$.port").Value.GetInt64();
            string db_name = jsonDocument.SelectElement("$.db_name").Value.GetString();
            return $"Host={host};Port={port};Database={db_name};Username={username};Password={password}";
        }, true);

        // Storage
        public static readonly string STORAGE_S3_BUCKET_NAME = GetEnvironmentVariable("STORAGE_S3_BUCKET_NAME", AWS_SECRET_S3, ".bucket_name");
        public static readonly string AWS_ACCESS_KEY_ID = GetEnvironmentVariable("AWS_ACCESS_KEY_ID", AWS_SECRET_S3, ".aws_access_key");
        public static readonly string AWS_SECRET_ACCESS_KEY = GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", AWS_SECRET_S3, ".aws_secret");
        public static readonly string AWS_DEFAULT_REGION = GetEnvironmentVariable("AWS_DEFAULT_REGION", "sa-east-1");

        // Notification
        public static readonly string NOTIFICATIONS_MOCK = GetEnvironmentVariable("NOTIFICATIONS_MOCK", "false");
        public static readonly string NOTIFICATIONS_BASE_URL = GetEnvironmentVariable("NOTIFICATIONS_BASE_URL", AWS_SECRET_NOTIFICATIONS, ".base_url");
        public static readonly string NOTIFICATIONS_CLIENT_ID = GetEnvironmentVariable("NOTIFICATIONS_CLIENT_ID", AWS_SECRET_NOTIFICATIONS, ".client_id");
        public static readonly string NOTIFICATIONS_CLIENT_SECRET = GetEnvironmentVariable("NOTIFICATIONS_CLIENT_SECRET", AWS_SECRET_NOTIFICATIONS, ".client_secret");
        public static readonly string NOTIFICATIONS_GRANT_TYPE = GetEnvironmentVariable("NOTIFICATIONS_GRANT_TYPE", AWS_SECRET_NOTIFICATIONS, ".grant_type");
        public static readonly string NOTIFICATIONS_AUTHENTICATION_KEY = GetEnvironmentVariable("NOTIFICATIONS_AUTHENTICATION_KEY", AWS_SECRET_NOTIFICATIONS, ".authentication_key");
        public static readonly string NOTIFICATIONS_FROM_NUMBER = GetEnvironmentVariable("NOTIFICATIONS_FROM_NUMBER", AWS_SECRET_NOTIFICATIONS, ".from_number");
        public static readonly string NOTIFICATIONS_GET_TOKEN = GetEnvironmentVariable("NOTIFICATIONS_GET_TOKEN", AWS_SECRET_NOTIFICATIONS, ".get_token");
        public static readonly string NOTIFICATIONS_SEND_API_URL = GetEnvironmentVariable("NOTIFICATIONS_SEND_API_URL", AWS_SECRET_NOTIFICATIONS, ".send_api_url");
        public static readonly string NOTIFICATIONS_GET_API_URL = GetEnvironmentVariable("NOTIFICATIONS_GET_API_URL", AWS_SECRET_NOTIFICATIONS, ".get_api_url");

        // JWT
        public static readonly string SECRET_JWT_TOKEN = GetEnvironmentVariable("SECRET_JWT_TOKEN", AWS_SECRET_JWT_TOKEN, ".jwt_token", true);

    }
}

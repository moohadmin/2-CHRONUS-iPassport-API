using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace iPassport.Api.Configurations.Converters
{
    public class EmptyStringJsonConverter : JsonConverter<string>
    {
        public bool CanRead => true;
        public bool CanWrite => false;
       
        public override bool CanConvert(Type objectType)
        {
            return typeof(string) == objectType;
        }

        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            value = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
            writer.WriteStringValue(value);
        }
    }
}

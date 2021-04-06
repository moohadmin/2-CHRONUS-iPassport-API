using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace iPassport.Api.Configurations.Converters
{
    /// <summary>
    /// Empty String Json Converter
    /// </summary>
    public class EmptyStringJsonConverter : JsonConverter<string>
    {
        /// <summary>
        /// Can Read
        /// </summary>
        public bool CanRead => true;
        /// <summary>
        /// Can Write
        /// </summary>
        public bool CanWrite => false;

        /// <summary>
        /// Can Convert Method
        /// </summary>
        /// <param name="objectType">Type of object</param>
        /// <returns>Indicator if object can be converted</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(string) == objectType;
        }

        /// <summary>
        /// Read Method
        /// </summary>
        /// <param name="reader">Json reader</param>
        /// <param name="typeToConvert">Type to Convert</param>
        /// <param name="options">Json Seralize Options</param>
        /// <returns>Readed string</returns>
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        /// <summary>
        /// Write method 
        /// </summary>
        /// <param name="writer">Json Writer</param>
        /// <param name="value">value to write</param>
        /// <param name="options">Json Seralize Options</param>
        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            value = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
            writer.WriteStringValue(value);
        }
    }
}

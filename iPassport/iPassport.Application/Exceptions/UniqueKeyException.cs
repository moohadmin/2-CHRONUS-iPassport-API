using System;
using System.Runtime.Serialization;

namespace iPassport.Application.Exceptions
{
    [Serializable]
    public class UniqueKeyException : Exception
    {
        public string Key { get; }
        public int HttpCode { get; set; }

        protected UniqueKeyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        
        public UniqueKeyException(string key, Exception exception, int httpCode = 400) : base("Ocorreu um ou mais erros ao persistir os dados.", exception)
        {
            Key = key;
            HttpCode = 400;
        }
    }
}

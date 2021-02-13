using System;
using System.Runtime.Serialization;

namespace iPassport.Application.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public int HttpCode { get; set; }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public BusinessException(string message, int httpCode = 400) : base(message) => HttpCode = httpCode;
    }
}

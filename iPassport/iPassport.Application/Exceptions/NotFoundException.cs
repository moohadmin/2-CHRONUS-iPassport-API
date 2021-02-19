using System;
using System.Runtime.Serialization;

namespace iPassport.Application.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public int HttpCode { get; set; }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public NotFoundException(string message, int httpCode = 404) : base(message) => HttpCode = httpCode;
    }
}

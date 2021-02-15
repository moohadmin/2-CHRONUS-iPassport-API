using System;
using System.Runtime.Serialization;

namespace iPassport.Application.Exceptions
{
    [Serializable]
    public class InternalErrorException : Exception
    {
        public string Trace { get; set; }
        public int HttpCode { get; set; }

        public InternalErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public InternalErrorException(string message, int httpCode, string trace = null) : base(message)
        {
            Trace = trace;
            HttpCode = httpCode;
        }
    }
}

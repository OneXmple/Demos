using System;

namespace Httpgrpc.Common.Exceptions
{
    public class HttpgrpcException : Exception
    {
        public string Code { get; }

        public HttpgrpcException()
        {
        }

        public HttpgrpcException(string code)
        {
            Code = code;
        }

        public HttpgrpcException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        public HttpgrpcException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        public HttpgrpcException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public HttpgrpcException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        
    }
}
using System;
using System.Runtime.Serialization;

namespace ToUs.Models
{
    [Serializable]
    internal class WrongPathException : Exception
    {
        public WrongPathException()
        {
        }

        public WrongPathException(string message = "The file path is wrong") : base(message)
        {
        }

        public WrongPathException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongPathException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
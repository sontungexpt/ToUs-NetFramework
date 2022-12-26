using System;
using System.Runtime.Serialization;

namespace ToUs.Models
{
    [Serializable]
    internal class ConnectionFailedException : Exception
    {
        public ConnectionFailedException(string message = "Can't connect to excel file, may be the excel file are opened in other process, or the path is not exited") : base(message)
        {
        }

        public ConnectionFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConnectionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
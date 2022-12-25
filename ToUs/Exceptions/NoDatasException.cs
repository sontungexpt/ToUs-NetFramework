using System;
using System.Runtime.Serialization;

namespace ToUs.Models
{
    [Serializable]
    internal class NoDatasException : Exception
    {
        public NoDatasException(string message = "Can't read the datas") : base(message)
        {
        }

        public NoDatasException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoDatasException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

[Serializable]
internal class NotExistedColumnException : Exception
{
    public NotExistedColumnException()
    {
    }

    public NotExistedColumnException(string message) : base(message)
    {
    }

    public NotExistedColumnException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected NotExistedColumnException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
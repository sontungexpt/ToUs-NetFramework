using System;
using System.Runtime.Serialization;

[Serializable]
internal class NotExistedPathException : Exception
{
    public NotExistedPathException(string message = "Path is existed") : base(message)
    {
    }

    public NotExistedPathException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected NotExistedPathException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
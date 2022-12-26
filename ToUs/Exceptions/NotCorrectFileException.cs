using System;
using System.Runtime.Serialization;

namespace ToUs.Models
{
    [Serializable]
    internal class NotCorrectFileException : Exception
    {

        public NotCorrectFileException(string message = "File nhập vào không hợp lệ vui lòng chọn đúng file thời khoá biểu") : base(message)
        {
        }

        public NotCorrectFileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotCorrectFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
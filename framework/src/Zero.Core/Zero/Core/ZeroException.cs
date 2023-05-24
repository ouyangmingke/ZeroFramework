using System.Runtime.Serialization;

namespace Zero.Core
{
    public class ZeroException : Exception
    {
        public ZeroException()
        {

        }

        public ZeroException(string message)
            : base(message)
        {

        }

        public ZeroException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public ZeroException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}

using System;
using System.Runtime.Serialization;

namespace Confluent.RestClient.Exceptions
{
    [Serializable]
    public abstract class ConfluentApiException : Exception
    {
        protected ConfluentApiException()
        {
        }

        protected ConfluentApiException(string message) : base(message)
        {
        }

        protected ConfluentApiException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ConfluentApiException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}

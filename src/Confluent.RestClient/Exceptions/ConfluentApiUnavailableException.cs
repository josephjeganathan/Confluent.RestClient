using System;
using System.Runtime.Serialization;

namespace Confluent.RestClient.Exceptions
{
    [Serializable]
    public class ConfluentApiUnavailableException : ConfluentApiException
    {
        public ConfluentApiUnavailableException()
        {
        }

        public ConfluentApiUnavailableException(string message) : base(message)
        {
        }

        public ConfluentApiUnavailableException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ConfluentApiUnavailableException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}

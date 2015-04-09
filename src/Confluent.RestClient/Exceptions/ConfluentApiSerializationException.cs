using System;
using System.Runtime.Serialization;

namespace Confluent.RestClient.Exceptions
{
    [Serializable]
    public class ConfluentApiSerializationException : ConfluentApiException
    {
        public ConfluentApiSerializationException()
        {
        }

        public ConfluentApiSerializationException(string message) : base(message)
        {
        }

        public ConfluentApiSerializationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ConfluentApiSerializationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}

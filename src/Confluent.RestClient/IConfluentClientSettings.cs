using System;

namespace Confluent.RestClient
{
    public interface IConfluentClientSettings
    {
        string KafkaBaseUrl { get; }
        TimeSpan RequestTimeout { get; }
    }
}

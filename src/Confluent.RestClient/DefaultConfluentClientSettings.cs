using System;

namespace Confluent.RestClient
{
    public class DefaultConfluentClientSettings : IConfluentClientSettings
    {
        public string KafkaBaseUrl
        {
            get { return "http://localhost:8082"; }
        }

        public TimeSpan RequestTimeout
        {
            get { return new TimeSpan(0,0,0,3); }
        }
    }
}

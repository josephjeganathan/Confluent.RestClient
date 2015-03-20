using Confluent.RestClient;

namespace Confluent.TestHarness
{
    public class ConfluentClientSettings : IConfluentClientSettings
    {
        public ConfluentClientSettings(string kafkaBaseUrl)
        {
            KafkaBaseUrl = kafkaBaseUrl;
        }

        public string KafkaBaseUrl { get; private set; }
    }
}

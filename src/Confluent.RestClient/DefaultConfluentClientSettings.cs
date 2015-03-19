namespace Confluent.RestClient
{
    public class DefaultConfluentClientSettings : IConfluentClientSettings
    {
        public string KafkaBaseUrl
        {
            get { return "http://localhost:8082"; }
        }
    }
}

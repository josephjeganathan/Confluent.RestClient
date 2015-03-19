using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class TopicConfiguration
    {
        [JsonProperty(PropertyName = "cleanup.policy")]
        public string CleanupPolicy { get; set; }
    }
}
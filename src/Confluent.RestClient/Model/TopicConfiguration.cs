using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    /// <summary>
    /// Topic configuration overrides
    /// </summary>
    public class TopicConfiguration
    {
        [JsonProperty(PropertyName = "cleanup.policy")]
        public string CleanupPolicy { get; set; }
    }
}
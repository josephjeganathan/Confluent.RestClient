using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class ConsumerOffset
    {
        [JsonProperty(PropertyName = "topic")]
        public string Topic { get; set; }

        [JsonProperty(PropertyName = "partition")]
        public int Partition { get; set; }

        [JsonProperty(PropertyName = "consumed")]
        public long Consumed { get; set; }

        [JsonProperty(PropertyName = "committed")]
        public string Committed { get; set; }
    }
}

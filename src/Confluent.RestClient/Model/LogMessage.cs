using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public abstract class LogMessage
    {
        [JsonProperty(PropertyName = "partition")]
        public int Partition { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public long Offset { get; set; }
    }
}

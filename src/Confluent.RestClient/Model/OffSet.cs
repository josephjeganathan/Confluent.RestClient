using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class OffSet
    {
        [JsonProperty(PropertyName = "partition")]
        public int PartitionId { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public long Offset { get; set; }
    }
}

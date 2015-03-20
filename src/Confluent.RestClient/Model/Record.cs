using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class Record<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        [JsonProperty(PropertyName = "key")]
        public TKey Key { get; set; }

        [JsonProperty(PropertyName = "partition")]
        public int? PartitionId { get; set; }

        [JsonProperty(PropertyName = "value")]
        public TValue Value { get; set; }
    }
}

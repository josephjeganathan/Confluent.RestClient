using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public abstract class Record<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        /// <summary>
        /// The message key, formatted according to the embedded format, or null to omit a key (optional)
        /// </summary>
        [JsonProperty(PropertyName = "key")]
        public TKey Key { get; set; }

        /// <summary>
        /// The message value, formatted according to the embedded format
        /// This can be 
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public TValue Value { get; set; }

        /// <summary>
        /// Partition to store the message in (optional)
        /// </summary>
        [JsonProperty(PropertyName = "partition")]
        public int? PartitionId { get; set; }
    }
}

using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class OffSet
    {
        /// <summary>
        /// Partition the message was published to, or null if publishing the message failed
        /// </summary>
        [JsonProperty(PropertyName = "partition")]
        public int PartitionId { get; set; }

        /// <summary>
        /// Offset of the message, or null if publishing the message failed
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        public long Offset { get; set; }

        /// <summary>
        /// An error code classifying the reason this operation failed, or null if it succeeded.
        /// 1 - Non-retriable Kafka exception
        /// 2 - Retriable Kafka exception; the message might be sent successfully if retried
        /// </summary>
        [JsonProperty(PropertyName = "error_code")]
        public long? ErrorCode { get; set; }
    }
}

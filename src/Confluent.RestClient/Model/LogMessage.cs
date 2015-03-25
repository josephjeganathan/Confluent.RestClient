using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public abstract class LogMessage
    {
        /// <summary>
        /// Partition of the message
        /// </summary>
        [JsonProperty(PropertyName = "partition")]
        public int Partition { get; set; }

        /// <summary>
        /// Offset of the message
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        public long Offset { get; set; }
    }
}

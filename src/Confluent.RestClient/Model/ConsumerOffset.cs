using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class ConsumerOffset
    {
        /// <summary>
        /// Name of the topic for which an offset was committed
        /// </summary>
        [JsonProperty(PropertyName = "topic")]
        public string Topic { get; set; }

        /// <summary>
        /// Partition ID for which an offset was committed
        /// </summary>
        [JsonProperty(PropertyName = "partition")]
        public int Partition { get; set; }

        /// <summary>
        /// The committed offset value. 
        /// If the commit was successful, this should be identical to `consumed`
        /// </summary>
        [JsonProperty(PropertyName = "committed")]
        public string Committed { get; set; }
        
        /// <summary>
        /// The offset of the most recently consumed message
        /// </summary>
        [JsonProperty(PropertyName = "consumed")]
        public long Consumed { get; set; }
    }
}

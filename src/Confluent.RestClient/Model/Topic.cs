using System.Collections.Generic;
using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    /// <summary>
    /// Topic metadata
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// Name of the topic
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Per-topic configuration overrides
        /// </summary>
        [JsonProperty(PropertyName = "configs")]
        public TopicConfiguration Configuration { get; set; }

        /// <summary>
        /// List of partitions for this topic
        /// </summary>
        [JsonProperty(PropertyName = "partitions")]
        public List<Partition> Partitions { get; set; }
    }
}

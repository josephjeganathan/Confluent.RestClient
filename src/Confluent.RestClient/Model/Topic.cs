using System.Collections.Generic;
using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class Topic
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "configs")]
        public TopicConfiguration Configuration { get; set; }

        [JsonProperty(PropertyName = "partitions")]
        public List<Partition> Partitions { get; set; }
    }
}

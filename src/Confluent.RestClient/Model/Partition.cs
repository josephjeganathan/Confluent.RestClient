using System.Collections.Generic;
using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class Partition
    {
        [JsonProperty(PropertyName = "partition")]
        public int PartitionId { get; set; }

        [JsonProperty(PropertyName = "leader")]
        public int LeaderId { get; set; }

        [JsonProperty(PropertyName = "replicas")]
        public List<Replica> Replicas { get; set; }
    }
}

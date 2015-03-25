using System.Collections.Generic;
using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    /// <summary>
    /// Partition metadata
    /// </summary>
    public class Partition
    {
        /// <summary>
        /// The ID of the partition
        /// </summary>
        [JsonProperty(PropertyName = "partition")]
        public int PartitionId { get; set; }

        /// <summary>
        /// The broker ID of the leader for this partition
        /// </summary>
        [JsonProperty(PropertyName = "leader")]
        public int LeaderId { get; set; }

        /// <summary>
        /// List of replicas for this partition, including the leader
        /// </summary>
        [JsonProperty(PropertyName = "replicas")]
        public List<Replica> Replicas { get; set; }
    }
}

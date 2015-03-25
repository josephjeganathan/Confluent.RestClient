using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class Replica
    {
        /// <summary>
        /// Broker ID of the replica
        /// </summary>
        [JsonProperty(PropertyName = "broker")]
        public int BrokerId { get; set; }

        /// <summary>
        /// True if this replica is the leader for the partition
        /// </summary>
        [JsonProperty(PropertyName = "leader")]
        public bool Leader { get; set; }

        /// <summary>
        /// True if this replica is currently in sync with the leader
        /// </summary>
        [JsonProperty(PropertyName = "in_sync")]
        public bool InSync { get; set; }
    }
}

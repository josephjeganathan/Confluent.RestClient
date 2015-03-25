using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class Replica
    {
        [JsonProperty(PropertyName = "broker")]
        public int BrokerId { get; set; }

        [JsonProperty(PropertyName = "leader")]
        public bool Leader { get; set; }

        [JsonProperty(PropertyName = "in_sync")]
        public bool InSync { get; set; }
    }
}

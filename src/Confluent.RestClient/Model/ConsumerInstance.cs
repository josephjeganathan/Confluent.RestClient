using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class ConsumerInstance
    {
        [JsonProperty(PropertyName = "instance_id")]
        public string InstanceId { get; set; }

        [JsonProperty(PropertyName = "base_uri")]
        public string BaseUri { get; set; }
    }
}

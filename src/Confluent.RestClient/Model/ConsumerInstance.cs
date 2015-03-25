using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class ConsumerInstance
    {
        /// <summary>
        /// Unique ID for the consumer instance in this group. 
        /// If provided in the initial request, this will be identical to `CreateConsumerRequest.InstanceId`
        /// </summary>
        [JsonProperty(PropertyName = "instance_id")]
        public string InstanceId { get; set; }

        /// <summary>
        /// Base URI used to construct URIs for subsequent requests against this consumer instance. 
        /// This will be of the form `http://hostname:port/consumers/consumer_group/instances/instance_id`
        /// </summary>
        [JsonProperty(PropertyName = "base_uri")]
        public string BaseUri { get; set; }
    }
}

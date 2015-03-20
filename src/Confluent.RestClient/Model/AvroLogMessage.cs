using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class AvroLogMessage<TKey, TValue> : LogMessage
        where TKey : class
        where TValue : class
    {
        [JsonProperty(PropertyName = "key")]
        public TKey Key { get; set; }

        [JsonProperty(PropertyName = "value")]
        public TValue Value { get; set; }
    }
}

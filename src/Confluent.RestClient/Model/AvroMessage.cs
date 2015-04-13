using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class AvroMessage<TKey, TValue> : Message
        where TKey : class
        where TValue : class
    {
        /// <summary>
        /// The message key, formatted according to the Avro schema
        /// </summary>
        [JsonProperty(PropertyName = "key")]
        public TKey Key { get; set; }

        /// <summary>
        /// The message value, formatted according to the Avro schema
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public TValue Value { get; set; }
    }
}

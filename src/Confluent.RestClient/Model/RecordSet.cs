using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public abstract class RecordSet
    {
        [JsonProperty(PropertyName = "key_schema_id")]
        public int? KeySchemaId { get; set; }

        [JsonProperty(PropertyName = "key_schema")]
        public string KeySchema { get; set; }

        [JsonProperty(PropertyName = "value_schema_id")]
        public int? ValueSchemaId { get; set; }

        [JsonProperty(PropertyName = "value_schema")]
        public string ValueSchema { get; set; }
    }
}
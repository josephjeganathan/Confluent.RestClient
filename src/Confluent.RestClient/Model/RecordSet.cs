using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public abstract class RecordSet
    {
        /// <summary>
        /// ID returned by a previous request using the same schema. 
        /// This ID corresponds to the ID of the schema in the registry
        /// </summary>
        [JsonProperty(PropertyName = "key_schema_id")]
        public int? KeySchemaId { get; set; }

        /// <summary>
        /// Full schema encoded as a string (e.g. JSON serialized for Avro data)
        /// </summary>
        [JsonProperty(PropertyName = "key_schema")]
        public string KeySchema { get; set; }

        /// <summary>
        /// ID returned by a previous request using the same schema. 
        /// This ID corresponds to the ID of the schema in the registry
        /// </summary>
        [JsonProperty(PropertyName = "value_schema_id")]
        public int? ValueSchemaId { get; set; }

        /// <summary>
        /// Full schema encoded as a string (e.g. JSON serialized for Avro data)
        /// </summary>
        [JsonProperty(PropertyName = "value_schema")]
        public string ValueSchema { get; set; }
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class PublishResponse
    {
        /// <summary>
        /// The ID for the schema used to produce keys, or null if keys were not used
        /// </summary>
        [JsonProperty(PropertyName = "key_schema_id")]
        public int? KeySchemaId { get; set; }

        /// <summary>
        /// The ID for the schema used to produce values
        /// </summary>
        [JsonProperty(PropertyName = "value_schema_id")]
        public int? ValueSchemaId { get; set; }

        /// <summary>
        /// List of partitions and offsets the messages were published to
        /// </summary>
        [JsonProperty(PropertyName = "offsets")]
        public List<OffSet> OffSets { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class PublishResponse
    {
        [JsonProperty(PropertyName = "key_schema_id")]
        public int? KeySchemaId { get; set; }

        [JsonProperty(PropertyName = "value_schema_id")]
        public int? ValueSchemaId { get; set; }

        [JsonProperty(PropertyName = "offsets")]
        public List<OffSet> OffSets { get; set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class RecordSet<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        public RecordSet()
        {
            Records = new List<Record<TKey, TValue>>();
        }

        public RecordSet(params Record<TKey, TValue>[] records)
        {
            Records = new List<Record<TKey, TValue>>(records);
        }

        [JsonProperty(PropertyName = "key_schema_id")]
        public int? KeySchemaId { get; set; }

        [JsonProperty(PropertyName = "key_schema")]
        public string KeySchema { get; set; }

        [JsonProperty(PropertyName = "value_schema_id")]
        public int? ValueSchemaId { get; set; }

        [JsonProperty(PropertyName = "value_schema")]
        public string ValueSchema { get; set; }

        [JsonProperty(PropertyName = "records")]
        public List<Record<TKey,TValue>> Records { get; private set; }
    }
}
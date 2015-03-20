using System.Collections.Generic;
using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class AvroRecordSet<TKey, TValue> : RecordSet
        where TKey : class
        where TValue : class
    {
        public AvroRecordSet(params AvroRecord<TKey, TValue>[] records)
        {
            Records = new List<Record<TKey, TValue>>(records);
        }

        [JsonProperty(PropertyName = "records")]
        public List<Record<TKey, TValue>> Records { get; private set; }
    }
}

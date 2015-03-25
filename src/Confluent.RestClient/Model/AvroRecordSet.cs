using System.Collections.Generic;
using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    /// <summary>
    /// Avro record set
    /// Information about Key and Value schemas must be provided
    /// When the key is optional `string` can be used as <see cref="TKey"/> 
    /// </summary>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of the value</typeparam>
    public class AvroRecordSet<TKey, TValue> : RecordSet
        where TKey : class
        where TValue : class
    {
        public AvroRecordSet(params AvroRecord<TKey, TValue>[] records)
        {
            Records = new List<Record<TKey, TValue>>(records);
        }

        /// <summary>
        /// A list of records to produce to the topic
        /// </summary>
        [JsonProperty(PropertyName = "records")]
        public List<Record<TKey, TValue>> Records { get; private set; }
    }
}

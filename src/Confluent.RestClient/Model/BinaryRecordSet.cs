using Newtonsoft.Json;
using System.Collections.Generic;

namespace Confluent.RestClient.Model
{
    /// <summary>
    /// Binary record set
    /// Information about Key and Value schemas must be empty or null for binary messages 
    /// </summary>
    public class BinaryRecordSet : RecordSet
    {
        public BinaryRecordSet(params BinaryRecord[] records)
        {
            Records = new List<BinaryRecord>(records);
        }

        /// <summary>
        /// A list of records to produce to the partition
        /// </summary>
        [JsonProperty(PropertyName = "records")]
        public List<BinaryRecord> Records { get; private set; }
    }
}

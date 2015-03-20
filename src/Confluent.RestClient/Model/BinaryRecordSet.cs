using Newtonsoft.Json;
using System.Collections.Generic;

namespace Confluent.RestClient.Model
{
    public class BinaryRecordSet : RecordSet
    {
        public BinaryRecordSet(params BinaryRecord[] records)
        {
            Records = new List<BinaryRecord>(records);
        }

        [JsonProperty(PropertyName = "records")]
        public List<BinaryRecord> Records { get; private set; }
    }
}

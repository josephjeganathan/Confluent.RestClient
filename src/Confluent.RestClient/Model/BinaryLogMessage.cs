using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class BinaryLogMessage : LogMessage
    {
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}

using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class CreateConsumerRequest
    {
        public CreateConsumerRequest()
        {
            MessageFormat = MessageFormat.Binary;
            AutoOffsetReset = "smallest";
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "format")]
        public string Format { get; private set; }

        [JsonProperty(PropertyName = "auto.offset.reset")]
        public string AutoOffsetReset { get; set; }

        [JsonProperty(PropertyName = "auto.commit.enable")]
        public bool AutoCommitEnabled { get; set; }

        [JsonIgnore]
        public MessageFormat MessageFormat
        {
            get { return Format == "binary" ? MessageFormat.Binary : MessageFormat.Avro; }
            set { Format = value == MessageFormat.Binary ? "binary" : "avro"; }
        }
    }
}

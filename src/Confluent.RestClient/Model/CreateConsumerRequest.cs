using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    /// <summary>
    /// Request to create a new consumer instance
    /// </summary>
    public class CreateConsumerRequest
    {
        public CreateConsumerRequest()
        {
            MessageFormat = MessageFormat.Binary;
            AutoOffsetReset = "smallest";
        }

        /// <summary>
        /// Unique ID for the consumer instance in this group. 
        /// If omitted, one will be automatically generated using the REST proxy ID and an auto-incrementing number
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string InstanceId { get; set; }

        /// <summary>
        /// The format of consumed messages, which is used to convert messages into a JSON-compatible form. 
        /// Valid values: “binary”, “avro”. If unspecified, defaults to “binary”
        /// </summary>
        [JsonProperty(PropertyName = "format")]
        public string Format { get; private set; }

        /// <summary>
        /// Sets the `auto.offset.reset` setting for the consumer
        /// </summary>
        [JsonProperty(PropertyName = "auto.offset.reset")]
        public string AutoOffsetReset { get; set; }

        /// <summary>
        /// Sets the `auto.commit.enable` setting for the consumer
        /// </summary>
        [JsonProperty(PropertyName = "auto.commit.enable")]
        public bool AutoCommitEnabled { get; set; }

        /// <summary>
        /// The format of consumed messages, which is used to convert messages into a JSON-compatible form.
        /// </summary>
        [JsonIgnore]
        public MessageFormat MessageFormat
        {
            get { return Format == "binary" ? MessageFormat.Binary : MessageFormat.Avro; }
            set { Format = value == MessageFormat.Binary ? "binary" : "avro"; }
        }
    }
}

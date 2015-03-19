using Newtonsoft.Json;

namespace Confluent.RestClient.Model
{
    public class Error
    {
        [JsonProperty(PropertyName = "error_code")]
        public int ErrorCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}

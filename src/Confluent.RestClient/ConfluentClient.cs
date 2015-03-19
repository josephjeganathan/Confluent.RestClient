using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Confluent.RestClient.Model;
using Newtonsoft.Json;

namespace Confluent.RestClient
{
    public class ConfluentClient : IConfluentClient
    {
        private const string ContentTypeKafkaBinary = "application/vnd.kafka.binary.v1+json";
        private const string ContentTypeKafkaAvro = "application/vnd.kafka.avro.v1+json";

        private readonly HttpClient _client;

        public ConfluentClient() : this(new DefaultConfluentClientSettings())
        {
        }

        public ConfluentClient(IConfluentClientSettings clientSettings)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(clientSettings.KafkaBaseUrl)
            };
        }

        public async Task<ConfluentResponse<List<string>>> GetTopicsAsync()
        {
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, "/topics");

            return await SendRequest<List<string>>(request);
        }

        public async Task<ConfluentResponse<Topic>> GetTopicMetadataAsync(string topicName)
        {
            string requestUri = string.Format("/topics/{0}", topicName);
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, requestUri);

            return await SendRequest<Topic>(request);
        }

        public async Task<ConfluentResponse<List<Partition>>> GetTopicPartitionsAsync(string topicName)
        {
            string requestUri = string.Format("/topics/{0}/partitions", topicName);
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, requestUri);

            return await SendRequest<List<Partition>>(request);
        }

        public async Task<ConfluentResponse<Partition>> GetTopicPartitionAsync(string topicName, int partitionId)
        {
            string requestUri = string.Format("/topics/{0}/partitions/{1}", topicName, partitionId);
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, requestUri);

            return await SendRequest<Partition>(request);
        }

        private static HttpRequestMessage CreateRequestMessage(HttpMethod method, string requestUri)
        {
            return new HttpRequestMessage(method, requestUri);
        }

        private async Task<ConfluentResponse<TResponse>> SendRequest<TResponse>(HttpRequestMessage request)
            where TResponse : class
        {
            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return ConfluentResponse<TResponse>.Success(await ReadResponseAs<TResponse>(response));
            }

            return ConfluentResponse<TResponse>.Failed(await ReadResponseAs<Error>(response));
        }

        private async Task<TResponse> ReadResponseAs<TResponse>(HttpResponseMessage responseMessage)
            where TResponse : class
        {
            using (Stream stream = await responseMessage.Content.ReadAsStreamAsync())
            using (var streamReader = new StreamReader(stream))
            using (var reader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();

                return serializer.Deserialize<TResponse>(reader);
            }
        }
    }
}

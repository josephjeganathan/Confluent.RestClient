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
        private readonly IConfluentClientSettings _clientSettings;
        private const string ContentTypeKafkaBinary = "application/vnd.kafka.binary.v1+json";
        private const string ContentTypeKafkaAvro = "application/vnd.kafka.avro.v1+json";
        private const string ContentTypeKafkaDefault = "application/vnd.kafka.v1+json";

        private readonly HttpClient _client;

        public ConfluentClient() : this(new DefaultConfluentClientSettings())
        {
        }

        public ConfluentClient(IConfluentClientSettings clientSettings)
        {
            _clientSettings = clientSettings;
            _client = new HttpClient();
        }

        public async Task<ConfluentResponse<List<string>>> GetTopicsAsync()
        {
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, "/topics");

            return await SendRequest<List<string>>(request);
        }

        public async Task<ConfluentResponse<Topic>> GetTopicMetadataAsync(string topic)
        {
            string requestUri = string.Format("/topics/{0}", topic);
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, requestUri);

            return await SendRequest<Topic>(request);
        }

        public async Task<ConfluentResponse<List<Partition>>> GetTopicPartitionsAsync(string topic)
        {
            string requestUri = string.Format("/topics/{0}/partitions", topic);
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, requestUri);

            return await SendRequest<List<Partition>>(request);
        }

        public async Task<ConfluentResponse<Partition>> GetTopicPartitionAsync(
            string topic, 
            int partitionId)
        {
            string requestUri = string.Format("/topics/{0}/partitions/{1}", topic, partitionId);
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, requestUri);

            return await SendRequest<Partition>(request);
        }

        public async Task<ConfluentResponse<PublishResponse>> PublishAsBinaryAsync(
            string topic, 
            BinaryRecordSet recordSet)
        {
            string requestUri = string.Format("/topics/{0}", topic);

            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Post, requestUri)
                .WithContent(recordSet, ContentTypeKafkaBinary);

            return await SendRequest<PublishResponse>(request);
        }

        public async Task<ConfluentResponse<PublishResponse>> PublishAsAvroAsync<TKey, TValue>(
            string topic, 
            AvroRecordSet<TKey, TValue> recordSet)
            where TKey : class
            where TValue : class
        {
            string requestUri = string.Format("/topics/{0}", topic);

            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Post, requestUri)
                .WithContent(recordSet, ContentTypeKafkaAvro);

            return await SendRequest<PublishResponse>(request);
        }

        public async Task<ConfluentResponse<ConsumerInstance>> CreateConsumerAsync(
            string consumerGroupName, 
            CreateConsumerRequest createConsumerRequest)
        {
            string requestUri = string.Format("/consumers/{0}", consumerGroupName);

            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Post, requestUri)
                .WithContent(createConsumerRequest, ContentTypeKafkaDefault)
                .WithHostHeader(_clientSettings.KafkaBaseUrl);

            return await SendRequest<ConsumerInstance>(request);
        }

        public async Task<ConfluentResponse<List<BinaryLogMessage>>> ConsumeAsBinaryAsync(
            ConsumerInstance consumerInstance, 
            string consumerGroupName, 
            string topic)
        {
            string requestUri = string.Format("/consumers/{0}/instances/{1}/topics/{2}", consumerGroupName, consumerInstance.InstanceId, topic);

            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, requestUri)
                .WithContentType(ContentTypeKafkaBinary)
                .WithHostHeader(consumerInstance.BaseUri);

            return await SendRequest<List<BinaryLogMessage>>(request);
        }

        public async Task<ConfluentResponse<List<AvroLogMessage<TKey, TValue>>>> ConsumeAsAvroAsync<TKey, TValue>(
            ConsumerInstance consumerInstance,
            string consumerGroupName,
            string topic)
            where TKey : class
            where TValue : class
        {
            string requestUri = string.Format("/consumers/{0}/instances/{1}/topics/{2}", consumerGroupName, consumerInstance.InstanceId, topic);

            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, requestUri)
                .WithContentType(ContentTypeKafkaAvro)
                .WithHostHeader(consumerInstance.BaseUri);

            return await SendRequest<List<AvroLogMessage<TKey, TValue>>>(request);
        }
        
        public async Task<ConfluentResponse<List<ConsumerOffset>>> CommitOffsetAsync(
            ConsumerInstance consumerInstance,
            string consumerGroupName)
        {
            string requestUri = string.Format("/consumers/{0}/instances/{1}/offsets", consumerGroupName, consumerInstance.InstanceId);

            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Post, requestUri)
                .WithContentType(ContentTypeKafkaDefault)
                .WithHostHeader(consumerInstance.BaseUri);

            return await SendRequest<List<ConsumerOffset>>(request);
        }

        public async Task<ConfluentResponse> DeleteConsumerAsync(
            ConsumerInstance consumerInstance,
            string consumerGroupName)
        {
            string requestUri = string.Format("/consumers/{0}/instances/{1}", consumerGroupName, consumerInstance.InstanceId);

            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Delete, requestUri)
                .WithContentType(ContentTypeKafkaDefault)
                .WithHostHeader(consumerInstance.BaseUri);

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return ConfluentResponse.Success();
            }

            return ConfluentResponse.Failed(await ReadResponseAs<Error>(response));
        }

        private HttpRequestMessage CreateRequestMessage(HttpMethod method, string requestUri, string baseUri = null)
        {
            requestUri = string.Format("{0}{1}", string.IsNullOrWhiteSpace(baseUri) ? _clientSettings.KafkaBaseUrl : baseUri, requestUri);

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

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Confluent.RestClient.Exceptions;
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

        public ConfluentClient() 
            : this(new DefaultConfluentClientSettings())
        {
        }

        public ConfluentClient(IConfluentClientSettings clientSettings)
        {
            _clientSettings = clientSettings;
            _client = new HttpClient {Timeout = clientSettings.RequestTimeout};
        }

        public async Task<ConfluentResponse<List<string>>> GetTopicsAsync()
        {
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, "/topics");

            return await ProcessRequest<List<string>>(request);
        }

        public async Task<ConfluentResponse<Topic>> GetTopicMetadataAsync(string topic)
        {
            string requestUri = string.Format("/topics/{0}", topic);
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, requestUri);

            return await ProcessRequest<Topic>(request);
        }

        public async Task<ConfluentResponse<List<Partition>>> GetTopicPartitionsAsync(string topic)
        {
            string requestUri = string.Format("/topics/{0}/partitions", topic);
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, requestUri);

            return await ProcessRequest<List<Partition>>(request);
        }

        public async Task<ConfluentResponse<Partition>> GetTopicPartitionAsync(
            string topic, 
            int partitionId)
        {
            string requestUri = string.Format("/topics/{0}/partitions/{1}", topic, partitionId);
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, requestUri);

            return await ProcessRequest<Partition>(request);
        }

        public async Task<ConfluentResponse<PublishResponse>> PublishAsBinaryAsync(
            string topic, 
            BinaryRecordSet recordSet)
        {
            string requestUri = string.Format("/topics/{0}", topic);

            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Post, requestUri)
                .WithContent(recordSet, ContentTypeKafkaBinary);

            return await ProcessRequest<PublishResponse>(request);
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

            return await ProcessRequest<PublishResponse>(request);
        }

        public async Task<ConfluentResponse<ConsumerInstance>> CreateConsumerAsync(
            string consumerGroupName, 
            CreateConsumerRequest createConsumerRequest)
        {
            string requestUri = string.Format("/consumers/{0}", consumerGroupName);

            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Post, requestUri)
                .WithContent(createConsumerRequest, ContentTypeKafkaDefault)
                .WithHostHeader(_clientSettings.KafkaBaseUrl);

            return await ProcessRequest<ConsumerInstance>(request);
        }

        public async Task<ConfluentResponse<List<BinaryMessage>>> ConsumeAsBinaryAsync(
            ConsumerInstance consumerInstance,
            string topic,
            int? maxBytes = null)
        {
            string requestUri = BuildConsumeRequestUri(topic, maxBytes);

            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, requestUri, consumerInstance.BaseUri)
                .WithContentType(ContentTypeKafkaBinary)
                .WithHostHeader(consumerInstance.BaseUri);

            return await ProcessRequest<List<BinaryMessage>>(request);
        }

        public async Task<ConfluentResponse<List<AvroMessage<TKey, TValue>>>> ConsumeAsAvroAsync<TKey, TValue>(
            ConsumerInstance consumerInstance,
            string topic,
            int? maxBytes = null)
            where TKey : class
            where TValue : class
        {
            string requestUri = BuildConsumeRequestUri(topic, maxBytes);

            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Get, requestUri, consumerInstance.BaseUri)
                .WithContentType(ContentTypeKafkaAvro)
                .WithHostHeader(consumerInstance.BaseUri);

            return await ProcessRequest<List<AvroMessage<TKey, TValue>>>(request);
        }

        public async Task<ConfluentResponse<List<ConsumerOffset>>> CommitOffsetAsync(ConsumerInstance consumerInstance)
        {
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Post, "/offsets", consumerInstance.BaseUri)
                .WithContentType(ContentTypeKafkaDefault)
                .WithHostHeader(consumerInstance.BaseUri);

            return await ProcessRequest<List<ConsumerOffset>>(request);
        }

        public async Task<ConfluentResponse> DeleteConsumerAsync(ConsumerInstance consumerInstance)
        {
            HttpRequestMessage request = CreateRequestMessage(HttpMethod.Delete, "", consumerInstance.BaseUri)
                .WithContentType(ContentTypeKafkaDefault)
                .WithHostHeader(consumerInstance.BaseUri);

            var response = await SendRequest(request);

            if (response.IsSuccessStatusCode)
            {
                return ConfluentResponse.Success();
            }

            return ConfluentResponse.Failed(await ReadResponseAs<Error>(response));
        }
        
        private HttpRequestMessage CreateRequestMessage(HttpMethod method, string requestUri, string baseUri = null)
        {
            baseUri = (string.IsNullOrWhiteSpace(baseUri) ? _clientSettings.KafkaBaseUrl : baseUri).TrimEnd('/', '\\');
            requestUri = string.Format("{0}{1}", baseUri, requestUri);

            return new HttpRequestMessage(method, requestUri);
        }
       
        private async Task<ConfluentResponse<TResponse>> ProcessRequest<TResponse>(HttpRequestMessage request)
            where TResponse : class
        {
            var response = await SendRequest(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    return ConfluentResponse<TResponse>.Success(await ReadResponseAs<TResponse>(response));
                }

                return ConfluentResponse<TResponse>.Failed(await ReadResponseAs<Error>(response));
            }
            catch (Exception e)
            {
                throw new ConfluentApiSerializationException("Failed to deserialize response", e);
            }
        }

        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
        {
            try
            {
                return await _client.SendAsync(request);
            }
            catch (TaskCanceledException ex)
            {
                throw new ConfluentApiUnavailableException(
                    "The operation has timed-out. The timeout period elapsed prior to completion of the operation or the server is not responding.",
                    ex);
            }
            catch (Exception e)
            {
                throw new ConfluentApiUnavailableException("Failed to send request to confluent REST API", e);
            }            
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

        private static string BuildConsumeRequestUri(string topic, int? maxBytes)
        {
            string requestUri = string.Format("/topics/{0}", topic);
            if (maxBytes.HasValue)
            {
                requestUri = string.Format("{0}?max_bytes={1}", requestUri, maxBytes.Value);
            }
            return requestUri;
        }

        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
            }
        }
    }
}

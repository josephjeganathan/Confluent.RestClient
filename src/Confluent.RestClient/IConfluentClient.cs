using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.RestClient.Model;

namespace Confluent.RestClient
{
    /// <summary>
    /// Restful client for Confluent REST API
    /// http://confluent.io/docs/current/kafka-rest/docs/api.html
    /// </summary>
    public interface IConfluentClient
    {
        Task<ConfluentResponse<List<string>>> GetTopicsAsync();
        Task<ConfluentResponse<Topic>> GetTopicMetadataAsync(string topicName);
        Task<ConfluentResponse<List<Partition>>> GetTopicPartitionsAsync(string topicName);
        Task<ConfluentResponse<Partition>> GetTopicPartitionAsync(string topicName, int partitionId);

        Task<ConfluentResponse<ConsumerInstance>> CreateConsumerAsync(
            string consumerGroupName, 
            CreateConsumerRequest createConsumerRequest);

        Task<ConfluentResponse<PublishResponse>> PublishAsBinaryAsync(
            string topicName,
            BinaryRecordSet recordSet);

        Task<ConfluentResponse<PublishResponse>> PublishAsAvroAsync<TKey, TValue>(
            string topicName, 
            AvroRecordSet<TKey, TValue> recordSet)
            where TKey : class
            where TValue : class;

        Task<ConfluentResponse<List<BinaryLogMessage>>> ConsumeAsBinaryAsync(
            ConsumerInstance consumerInstance,
            string consumerGroupName,
            string topic);

        Task<ConfluentResponse<List<AvroLogMessage<TKey, TValue>>>> ConsumeAsAvroAsync<TKey, TValue>(
            ConsumerInstance consumerInstance,
            string consumerGroupName,
            string topic)
            where TKey : class
            where TValue : class;
    }
}
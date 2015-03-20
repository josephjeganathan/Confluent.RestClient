using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.RestClient.Model;

namespace Confluent.RestClient
{
    public interface IConfluentClient
    {
        Task<ConfluentResponse<List<string>>> GetTopicsAsync();
        Task<ConfluentResponse<Topic>> GetTopicMetadataAsync(string topicName);
        Task<ConfluentResponse<List<Partition>>> GetTopicPartitionsAsync(string topicName);
        Task<ConfluentResponse<Partition>> GetTopicPartitionAsync(string topicName, int partitionId);

        Task<ConfluentResponse<PublishResponse>> PublishAsBinaryAsync<TKey>(
            string topicName, 
            RecordSet<TKey, string> recordSet)
            where TKey : class;

        Task<ConfluentResponse<PublishResponse>> PublishAsAvroAsync<TKey, TValue>(
            string topicName, 
            RecordSet<TKey, TValue> recordSet)
            where TKey : class
            where TValue : class;
    }
}
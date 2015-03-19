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
    }
}
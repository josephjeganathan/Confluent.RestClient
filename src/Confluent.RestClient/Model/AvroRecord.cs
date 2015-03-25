namespace Confluent.RestClient.Model
{
    /// <summary>
    /// Avro record to publish to topic
    /// </summary>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of the value</typeparam>
    public class AvroRecord<TKey, TValue> : Record<TKey, TValue>
        where TKey : class
        where TValue : class
    {
    }
}

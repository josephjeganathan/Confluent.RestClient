namespace Confluent.RestClient.Model
{
    public class AvroRecord<TKey, TValue> : Record<TKey, TValue>
        where TKey : class
        where TValue : class
    {
    }
}

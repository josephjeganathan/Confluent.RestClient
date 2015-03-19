namespace Confluent.RestClient.Model
{
    public class ConfluentResponse<TPayload> where TPayload : class
    {
        public TPayload Payload { get; private set; }
        public Error Error { get; private set; }

        public static ConfluentResponse<TPayload> Success(TPayload payload)
        {
            return new ConfluentResponse<TPayload> { Payload = payload };
        }

        public static ConfluentResponse<TPayload> Failed(Error error)
        {
            return new ConfluentResponse<TPayload> { Error = error };
        }
    }
}

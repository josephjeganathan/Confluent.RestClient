namespace Confluent.RestClient.Model
{
    public class ConfluentResponse<TPayload> : ConfluentResponse where TPayload : class
    {
        protected ConfluentResponse()
        {
        }

        public TPayload Payload { get; private set; }

        public static ConfluentResponse<TPayload> Success(TPayload payload)
        {
            return new ConfluentResponse<TPayload> { Payload = payload };
        }

        public new static ConfluentResponse<TPayload> Failed(Error error)
        {
            return new ConfluentResponse<TPayload> { Error = error };
        }
    }

    public class ConfluentResponse
    {
        protected ConfluentResponse()
        {
        }

        public Error Error { get; protected set; }

        public static ConfluentResponse Success()
        {
            return new ConfluentResponse();
        }

        public static ConfluentResponse Failed(Error error)
        {
            return new ConfluentResponse { Error = error };
        }

        public bool IsSuccess()
        {
            return Error == null;
        }
    }
}

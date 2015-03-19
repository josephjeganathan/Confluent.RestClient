using System.Net.Http;
using System.Net.Http.Headers;

namespace Confluent.RestClient
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage WithContentType(this HttpRequestMessage requestMessage, string contentType)
        {
            requestMessage.Headers.Accept.Clear();
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

            return requestMessage;
        }
    }
}

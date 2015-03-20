using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Confluent.RestClient
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage WithContent<TContent>(this HttpRequestMessage requestMessage, 
            TContent content, 
            string contentType)
            where TContent : class
        {
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, contentType);
            
            return requestMessage;
        }

        public static HttpRequestMessage WithContentType(this HttpRequestMessage requestMessage, string contentType)
        {
            requestMessage.Headers.Accept.Clear();
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

            return requestMessage;
        }
    }
}

using System.Net.Http;

namespace Xefiros.Client.Helpers
{
    public class HttpClientConToken
    {
        public HttpClient HttpClient { get; }

        public HttpClientConToken(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
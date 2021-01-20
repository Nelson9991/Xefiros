using System.Net.Http;

namespace Xefiros.Client.Helpers
{
    public class HttpClientSinToken
    {
        public HttpClient HttpClient { get; }

        public HttpClientSinToken(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
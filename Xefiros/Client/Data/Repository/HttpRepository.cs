using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xefiros.Client.Data.Repository.IRepository;
using Xefiros.Client.Helpers;

namespace Xefiros.Client.Data.Repository
{
    public class HttpRepository : IHttpRepository
    {
        private readonly HttpClientConToken _httpClientConToken;
        private readonly HttpClientSinToken _httpClientSinToken;

        private readonly JsonSerializerOptions _defaultSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public HttpRepository(HttpClientConToken httpClientConToken, HttpClientSinToken httpClientSinToken)
        {
            _httpClientConToken = httpClientConToken;
            _httpClientSinToken = httpClientSinToken;
        }

        public async Task<HttpResponseWrapper<T>> Get<T>(string url, bool incluirToken = true)
        {
            var httpClient = incluirToken ? _httpClientConToken.HttpClient : _httpClientSinToken.HttpClient;
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var deserializedResp = await DeserializeResponse<T>(response, _defaultSerializerOptions);
                return new HttpResponseWrapper<T>(deserializedResp, false, response);
            }

            return new HttpResponseWrapper<T>(default, true, response);
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T resource)
        {
            var response = await _httpClientConToken.HttpClient.PostAsJsonAsync(url, resource);
            return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T resource)
        {
            var response = await _httpClientConToken.HttpClient.PostAsJsonAsync(url, resource);

            if (!response.IsSuccessStatusCode) return new HttpResponseWrapper<TResponse>(default, true, response);

            var deserializedResponse = await DeserializeResponse<TResponse>(response, _defaultSerializerOptions);

            return new HttpResponseWrapper<TResponse>(deserializedResponse, false, response);
        }

        public async Task<HttpResponseWrapper<TResponse>> Put<T, TResponse>(string url, T resource)
        {
            var response = await _httpClientConToken.HttpClient.PutAsJsonAsync(url, resource);
            if (!response.IsSuccessStatusCode) return new HttpResponseWrapper<TResponse>(default, true, response);

            var deserializedResponse = await DeserializeResponse<TResponse>(response, _defaultSerializerOptions);

            return new HttpResponseWrapper<TResponse>(deserializedResponse, false, response);
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T resource)
        {
            var response = await _httpClientConToken.HttpClient.PutAsJsonAsync(url, resource);
            return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var response = await _httpClientConToken.HttpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<TResponse>> Delete<TResponse>(string url)
        {
            var response = await _httpClientConToken.HttpClient.DeleteAsync(url);
            if (!response.IsSuccessStatusCode) return new HttpResponseWrapper<TResponse>(default, true, response);

            var deserializedResponse = await DeserializeResponse<TResponse>(response, _defaultSerializerOptions);

            return new HttpResponseWrapper<TResponse>(deserializedResponse, false, response);
        }

        private static async Task<T> DeserializeResponse<T>(HttpResponseMessage httpResponseMessage,
            JsonSerializerOptions
                jsonSerializerOptions)
        {
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(responseString, jsonSerializerOptions);
            return result;
        }
    }
}
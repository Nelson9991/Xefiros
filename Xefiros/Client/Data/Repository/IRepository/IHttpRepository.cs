using System.Threading.Tasks;
using Xefiros.Client.Helpers;

namespace Xefiros.Client.Data.Repository.IRepository
{
    public interface IHttpRepository
    {
        public Task<HttpResponseWrapper<T>> Get<T>(string url, bool incluirToken = true);
        public Task<HttpResponseWrapper<object>> Post<T>(string url, T resource);
        public Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T resource);
        public Task<HttpResponseWrapper<object>> Put<T>(string url, T resource);
        public Task<HttpResponseWrapper<TResponse>> Put<T, TResponse>(string url, T resource);
        public Task<HttpResponseWrapper<object>> Delete(string url);
        public Task<HttpResponseWrapper<TResponse>> Delete<TResponse>(string url);
    }
}
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string requestUri)
        {
            var response = await httpClient.GetAsync(requestUri);

            return await response.Content.ReadAsAsync<T>();
        }

        public static async Task<T> PostAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent)
        {
            var response = await httpClient.PostAsync(requestUri,httpContent);

            return await response.Content.ReadAsAsync<T>();
        }

        public static async Task<T> PutAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent)
        {
            var response = await httpClient.PutAsync(requestUri, httpContent);

            return await response.Content.ReadAsAsync<T>();
        }
        
        public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, string requestUri)
        {
            var response = await httpClient.DeleteAsync(requestUri);

            return await response.Content.ReadAsAsync<T>();
        }
    }
}

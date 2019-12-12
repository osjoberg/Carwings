using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Carwings
{
    internal class HttpClientWrapper : IDisposable
    {
        private readonly HttpClient httpClient = new HttpClient();

        public void Dispose()
        {
            httpClient.Dispose();
        }

        public virtual Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return httpClient.PostAsync(requestUri, content);
        }
    }
}

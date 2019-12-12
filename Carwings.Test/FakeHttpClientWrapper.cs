using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Carwings.Test
{
    internal class FakeHttpClientWrapper : HttpClientWrapper
    {
        private readonly Dictionary<string, HttpResponseMessage> _responses = new Dictionary<string, HttpResponseMessage>();

        public FakeHttpClientWrapper(string content, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            AddResponse("", content, statusCode);
        }

        public override Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            var responseMessage = _responses.ContainsKey(requestUri) ? _responses[requestUri] : _responses[""];

            return Task.Run(() => responseMessage);
        }

        public void AddResponse(string requestUri, string content, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            _responses.Add(requestUri, new HttpResponseMessage(statusCode) { Content = new StringContent(content) });
        }
    }
}

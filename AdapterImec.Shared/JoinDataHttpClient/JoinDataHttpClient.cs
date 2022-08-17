using System.Net.Http.Headers;

namespace AdapterImec.Shared.JoinDataHttpClient
{
    public class JoinDataHttpClient : IJoinDataHttpClient
    {
        private readonly HttpClient _httpClient;

        public JoinDataHttpClient()
        {
            _httpClient = new HttpClient();
        }

        public HttpRequestHeaders DefaultRequestHeaders => _httpClient.DefaultRequestHeaders;

        public Task<HttpResponseMessage> GetAsync(string? requestUri) => _httpClient.GetAsync(requestUri); 
    }
}

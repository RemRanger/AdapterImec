using System.Net.Http.Headers;

namespace AdapterImec.Shared.JoinDataHttpClient
{
    public interface IJoinDataHttpClient
    {
        HttpRequestHeaders DefaultRequestHeaders { get; }

        Task<HttpResponseMessage> GetAsync(string? requestUri);
        Task<HttpResponseMessage> PostAsync(string? requestUri, HttpContent? content);
    }
}

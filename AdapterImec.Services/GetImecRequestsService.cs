using AdapterImec.Shared;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AdapterImec.Services
{
    public class GetImecRequestsService : IGetImecRequestsService
    {
        private readonly ImecSettings _settings;

        public GetImecRequestsService(IOptions<ImecSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<JsonDocument> GetPendingRequestsAsync(string dataSourceId)
        {
            var token = await GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new HttpRequestException("Imec API GET pending requests: failed to get token");
            }

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = $@"{_settings.BaseUrl}/providers/{_settings.ProviderId}/sources/{dataSourceId}/requests/pending";
            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Imec API GET pending requests returned: {(int)response.StatusCode} {response.ReasonPhrase}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            return doc;
        }

        private async Task<string> GetToken()
        {
            string token = string.Empty;

            try
            {
                var tokenUrl = _settings.TokenUrl;
                var clientId = _settings.ClientId!;
                var clientSecret = _settings.ClientSecret!;

                var credentials = new
                {
                    clientId,
                    clientSecret,
                };
                var json = JsonSerializer.Serialize(credentials);
                var httpTokenContent = new StringContent(json);
                httpTokenContent!.Headers!.ContentType!.MediaType = "application/json";

                var httpClientToken = new HttpClient();

                var response = await httpClientToken.PostAsync(tokenUrl, httpTokenContent);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to get token: {response}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var responseDoc = JsonDocument.Parse(responseContent);
                token = responseDoc.RootElement.GetProperty("accessToken").GetString() ?? string.Empty;

                Console.WriteLine(!string.IsNullOrWhiteSpace(token) ? $"Getting token successful." : "Token not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Getting token failed: {ex.GetType()} - {ex.Message}");
            }
            finally
            {
                Console.WriteLine();
            }

            return token;
        }
    }
}

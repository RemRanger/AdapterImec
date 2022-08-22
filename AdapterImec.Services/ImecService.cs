using AdapterImec.Shared;
using AdapterImec.Shared.JoinDataHttpClient;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AdapterImec.Services;

public class ImecService : IImecService
{
    private readonly ImecSettings _settings;
    private readonly IJoinDataHttpClient _httpClient;
    private readonly IImecTokenService _imecTokenService;

    public ImecService(IOptions<ImecSettings> options, IJoinDataHttpClient httpClient, IImecTokenService imecTokenService)
    {
        _settings = options.Value;
        _httpClient = httpClient;
        _imecTokenService = imecTokenService;
    }

    public async Task<JsonDocument> GetPendingRequestsAsync(string dataSourceId)
    {
        var token = await _imecTokenService.GetTokenAsync();
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new HttpRequestException("Imec API GET pending requests: failed to get token");
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var url = $@"{_settings.BaseUrl}/providers/{_settings.ProviderId}/sources/{dataSourceId}/requests/pending";
        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Imec API GET pending requests returned: {(int)response.StatusCode} {response.ReasonPhrase}");
        }

        try
        {
            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);

            return doc;
        }
        catch (JsonException ex)
        {
            throw new HttpRequestException($"The response to Imec API GET pending requests contains invalid JSON", ex);
        }
        catch (Exception ex)
        {
            throw new HttpRequestException($"Failed to read response's JSON", ex);
        }
    }
}

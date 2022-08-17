using AdapterImec.Shared;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AdapterImec.Services;

public class GetImecRequestsService : IGetImecRequestsService
{
    private readonly ImecSettings _settings;
    private readonly IImecTokenService _imecTokenService;

    public GetImecRequestsService(IOptions<ImecSettings> options, IImecTokenService imecTokenService)
    {
        _settings = options.Value;
        _imecTokenService = imecTokenService;
    }

    public async Task<JsonDocument> GetPendingRequestsAsync(string dataSourceId)
    {
        var token = await _imecTokenService.GetTokenAsync();
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
}

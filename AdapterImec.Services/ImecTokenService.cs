﻿using AdapterImec.Shared;
using AdapterImec.Shared.JoinDataHttpClient;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text.Json;

namespace AdapterImec.Services;

public class ImecTokenService : IImecTokenService
{
    private readonly ImecSettings _settings;
    private readonly IJoinDataHttpClient _httpClient;
    private readonly double _tokenExpiryBufferTime = 15;
    private JwtSecurityToken? _decodedToken;
    private string? _originalToken;

    public ImecTokenService(IOptions<ImecSettings> options, IJoinDataHttpClient httpClient)
    {
        _settings = options.Value;
        _httpClient = httpClient;
    }

    public async Task<string?> GetTokenAsync()
    {
        if (_decodedToken is null || IsTokenExpired())
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

            var response = await _httpClient.PostAsync(tokenUrl, httpTokenContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseDoc = JsonDocument.Parse(responseContent);
                _originalToken = responseDoc.RootElement.GetProperty("accessToken").GetString() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(_originalToken))
                {
                    _decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(_originalToken);
                }
            }
        }

        return _originalToken;
    }

    private bool IsTokenExpired() => DateTime.UtcNow + TimeSpan.FromSeconds(_tokenExpiryBufferTime) > _decodedToken!.ValidTo;
}

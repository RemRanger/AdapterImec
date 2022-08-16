﻿using AdapterImec.Shared;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace AdapterImec.Services
{
    public class GetImecRequestsService : IGetImecRequestsService
    {
        public async Task<JsonDocument> GetPendingRequestsAsync(ImecSettings imecSettings, string dataSourceId)
        {
            var token = await GetToken(imecSettings);
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new HttpRequestException("Imec API GET pending requests: failed to get token");
            }

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = $@"{imecSettings.BaseUrl}/providers/{imecSettings.ProviderId}/sources/{dataSourceId}/requests/pending";
            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Imec API GET pending requests returned: {(int)response.StatusCode} {response.ReasonPhrase}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            return doc;
        }

        private static async Task<string> GetToken(ImecSettings imecSettings)
        {
            string token = string.Empty;

            try
            {
                var tokenUrl = imecSettings.TokenUrl;
                var clientId = imecSettings.ClientId!;
                var clientSecret = imecSettings.ClientSecret!;

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

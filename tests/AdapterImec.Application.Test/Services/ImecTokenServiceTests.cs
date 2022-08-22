using AdapterImec.Services;
using AdapterImec.Shared;
using AdapterImec.Shared.JoinDataHttpClient;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AdapterImec.Application.Test.Services
{
    public class ImecTokenServiceTests
    {
        private const string Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjJaUXBKM1VwYmpBWVhZR2FYRUpsOGxWMFRPSSJ9.eyJhdWQiOiI4MjNkZjAwOS01MGZjLTQ1Y2ItYWE3My1lM2U5ODc1MDcxYmMiLCJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vMjM4MmI2ZTQtODQyNC00Mzc5LWE2MTItODI2ZGZhZTM3YWEyL3YyLjAiLCJpYXQiOjE2NjExNTk0NDUsIm5iZiI6MTY2MTE1OTQ0NSwiZXhwIjoxNjYxMTYzMzQ1LCJhaW8iOiJFMlpnWUxCZCtNK1BNNHZ6TXVldmlZRmNQZy8vQWdBPSIsImF6cCI6ImJlNWJiOThiLTQ5OWItNGU1My05M2FkLWNkMzM0YWNmZWYxYSIsImF6cGFjciI6IjEiLCJvaWQiOiI2MzFmYjgwNy1kNjA3LTQwM2ItOWE4YS0wMzdkNTE3YjI0NWUiLCJyaCI6IjAuQVIwQTVMYUNJeVNFZVVPbUVvSnQtdU42b2dud1BZTDhVTXRGcW5QajZZZFFjYndkQUFBLiIsInN1YiI6IjYzMWZiODA3LWQ2MDctNDAzYi05YThhLTAzN2Q1MTdiMjQ1ZSIsInRpZCI6IjIzODJiNmU0LTg0MjQtNDM3OS1hNjEyLTgyNmRmYWUzN2FhMiIsInV0aSI6Ikt3bzJQc2RxaDAtb1ktejRvclFGQUEiLCJ2ZXIiOiIyLjAifQ.ZybteJwtNx89LQDh_jDYzODXm75AU270MpIbokXZ5ScsruJx2qKuWbDUeanzPL3Ax8jty7cmf7-WB3RrCr0I6t0NNt-M_HgF56lx7yQHIUOqn-L3uBu45l08uBheRtPpos7XgRoIVLVnIP98007N-0Tm9buiZMofv33VNu1ZaRZpkoUIkiG7V7OMGILU_6Lj9XUAwjIptRaKc5hGtX7nUAhtEa3Dt74xuMARQTinV3oUPkBiVJ4XccE3Tc_3spAnPqkz3Syoqx9_Ov2m2fQM6e9yZK8eT5Hb2TI0WPeW-wzKGeCtgYfXlLfFjos5qsmHVM0ySAipagm_MCmf0T_VEw";
        private const string BaseUrl = "www.la-vache-futile.fr";
        private const string ProviderId = "fournisseur-futile";

        private readonly ImecTokenService _imecTokenService;
        private readonly Mock<IJoinDataHttpClient> _httpClient;

        public ImecTokenServiceTests()
        {
            _httpClient = new Mock<IJoinDataHttpClient>();

            var options = new Mock<IOptions<ImecSettings>>();
            options.Setup(o => o.Value).Returns(new ImecSettings { BaseUrl = BaseUrl, ProviderId = ProviderId });

            _imecTokenService = new ImecTokenService(options.Object, _httpClient.Object);
        }

        [Fact]
        public async Task Should_Return_New_Token_When_None_Cached()
        {
            // Arrange
            var tokenJson = $@"{{""accessToken"": ""{Token}""}}";
            var tokenContent = new StringContent(tokenJson);
            _httpClient.Setup(h => h.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = tokenContent });

            // Act
            var token = await _imecTokenService.GetTokenAsync();

            // Assert
            token.Should().Be(Token);
        }
    }
}

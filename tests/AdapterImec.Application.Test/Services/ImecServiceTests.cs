using AdapterImec.Services;
using AdapterImec.Shared;
using AdapterImec.Shared.JoinDataHttpClient;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace AdapterImec.Application.Test.Services;

public class ImecServiceTests
{
    private const string Token = "toktok.alweer.een.ey";
    private const string DataSourceId = "source-de-dates-futile";
    private const string BaseUrl = "www.cat.com";
    private const string ProviderId = "fournisseur-futile";

    private readonly Mock<IImecTokenService> _imecTokenService;
    private readonly Mock<IJoinDataHttpClient> _httpClient;
    private readonly ImecService _imecService;

    public ImecServiceTests()
    {
        _imecTokenService = new Mock<IImecTokenService>();


        _httpClient = new Mock<IJoinDataHttpClient>();
        _httpClient.Setup(h => h.DefaultRequestHeaders).Returns(new HttpClient().DefaultRequestHeaders);

        var options = new Mock<IOptions<ImecSettings>>();
        options.Setup(o => o.Value).Returns(new ImecSettings { BaseUrl = BaseUrl, ProviderId = ProviderId });
        
        _imecService = new ImecService(options.Object, _httpClient.Object, _imecTokenService.Object);
    }

    [Fact]
    public async Task GetPendingRequestsAsync_Should_Return_JsonDocument_On_Happy_Flow()
    {
        // Arrange
        _imecTokenService.Setup(s => s.GetTokenAsync()).ReturnsAsync(Token);
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        _httpClient.Setup(h => h.GetAsync(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("{}")});

        // Act
        var jsonDocument = await _imecService.GetPendingRequestsAsync(DataSourceId);

        // ASsert
        jsonDocument.Should().NotBeNull();

    }
}

using AdapterImec.Api.Controllers;
using AdapterImec.Api.Models;
using AdapterImec.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace AdapterImec.Application.Test.Messages.Controllers;

public class AdapterImecControllerTests
{
    private const string DataSourceId = "source-de-dates-futile";
    private const string ExceptionMessage = "Allez Pouffe";

    private readonly Mock<IImecService> _imecService;
    private readonly AdapterImecController _adapterImecController;

    public AdapterImecControllerTests()
    {
        _imecService = new Mock<IImecService>();
        _adapterImecController = new AdapterImecController(_imecService.Object);
    }

    [Fact]
    public async Task GetPendingRequestsAsync_Should_Return_Ok_On_Happy_Flow()
    {
        // Arrange
        var jsonDocument = JsonDocument.Parse("{}");
        _imecService.Setup(s => s.GetPendingRequestsAsync(DataSourceId)).ReturnsAsync(jsonDocument);

        // Act
        var result = await _adapterImecController.GetPendingRequests(DataSourceId);

        // Assert
        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(DatahubResponseModelFactory.Create(jsonDocument));
    }

    [Fact]
    public async Task GetPendingRequestsAsync_Should_Return_BadRequest_On_Crappy_Flow()
    {
        // Arrange
        var jsonDocument = JsonDocument.Parse("{}");
        _imecService.Setup(s => s.GetPendingRequestsAsync(DataSourceId)).Throws(new Exception(ExceptionMessage));

        // Act
        var result = await _adapterImecController.GetPendingRequests(DataSourceId);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeEquivalentTo(ExceptionMessage);
    }
}

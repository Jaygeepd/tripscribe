using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using Tripscribe.Api.Integration.Test.Base;
using tripscribe.Api.Integration.Test.Models;
using tripscribe.Api.Integration.Test.TestUtilities;
using Tripscribe.Api.Integration.Test.TestUtilities;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Api.ViewModels.Stop;
using tripscribe.Dal.Models;
using Xunit.Abstractions;

namespace tripscribe.Api.Integration.Test.Controllers;

[Collection("Integration")]
public class StopControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _testOutputHelper;

    public StopControllerTests(ITestOutputHelper testOutputHelper, IntegrationClassFixture integrationFixture)
    {
        _testOutputHelper = testOutputHelper;
        _httpClient = integrationFixture.Host.CreateClient();
    }

    [Fact]
    public async Task GetAllStops_WhenStopsPresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync("/stop/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<StopViewModel[]>());
    }

    [Fact]
    public async Task GetAStopById_WhenStopPresent_ReturnsOk()
    {
        const int id = 1;
        var response = await _httpClient.GetAsync($"/stop/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<StopDetailViewModel>());

        Assert.Contains("1", value);
        Assert.Contains("Paris", value);
    }

    [Fact]
    public async Task CreateAStop_WhenStopDetailsValidAndPresent_ReturnsOk()
    {
        const string name = "Nice";
        const int journeyId = 1;
        
        CreateStopViewModel newStop = new CreateStopViewModel()
        {
            Name = name,
            JourneyId = journeyId,
            DateArrived = DateTime.Now - TimeSpan.FromDays(5),
            DateDeparted = DateTime.Now - TimeSpan.FromDays(4)
        };
        
        var response = await _httpClient.PostAsJsonAsync($"/stop/", newStop);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateAStop_WhenStopDetailsInvalid_ReturnsErrorMessage()
    {
        const string name = "";
        const int journeyId = 1;
        
        CreateStopViewModel newStop = new CreateStopViewModel()
        {
            Name = name,
            JourneyId = journeyId,
            DateArrived = DateTime.Now - TimeSpan.FromDays(5),
            DateDeparted = DateTime.Now - TimeSpan.FromDays(4)
        };
        
        var response = await _httpClient.PostAsJsonAsync($"/stop/", newStop);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("Name", "Stop name be entered, and between 4 and 100 characters in length");
        
        _testOutputHelper.WriteLine(value);
    }

    [Fact]
    public async Task UpdateAStop_WhenNewStopDetailsValidAndPresent_ReturnsOk()
    {
        const int id = 1;
        const string newName = "Bordeaux";

        UpdateStopViewModel updateStop = new UpdateStopViewModel()
        {
            Id = id,
            Name = newName,
            DateArrived = DateTime.Now - TimeSpan.FromDays(7),
            DateDeparted = DateTime.Now - TimeSpan.FromDays(6)
        };

        var response = await _httpClient.PatchAsJsonAsync($"/stop/{id}", updateStop);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task UpdateAStop_WhenNewStopDetailsInvalidData_ReturnsErrorMessage()
    {
        const int id = 1;
        const string newName = "";

        UpdateStopViewModel updateStop = new UpdateStopViewModel()
        {
            Id = id,
            Name = newName,
            DateArrived = DateTime.Now - TimeSpan.FromDays(7),
            DateDeparted = DateTime.Now - TimeSpan.FromDays(6)
        };

        var response = await _httpClient.PatchAsJsonAsync($"/stop/{id}", updateStop);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("Name", "Stop name be entered, and between 4 and 100 characters in length");
        
        _testOutputHelper.WriteLine(value);
    }

    [Fact]
    public async Task DeleteAStop_WhenStopFoundThenDeleted_ReturnsOk()
    {
        const int id = 3;

        var response = await _httpClient.DeleteAsync($"/stop/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
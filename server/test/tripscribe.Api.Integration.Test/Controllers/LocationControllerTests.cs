using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using Tripscribe.Api.Integration.Test.Base;
using tripscribe.Api.Integration.Test.TestUtilities;
using Tripscribe.Api.Integration.Test.TestUtilities;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Api.ViewModels.Locations;
using tripscribe.Api.ViewModels.Stop;
using tripscribe.Dal.Models;
using Xunit.Abstractions;

namespace tripscribe.Api.Integration.Test.Controllers;

[Collection("Integration")]
public class LocationControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _testOutputHelper;

    public LocationControllerTests(ITestOutputHelper testOutputHelper, IntegrationClassFixture integrationFixture)
    {
        _testOutputHelper = testOutputHelper;
        _httpClient = integrationFixture.Host.CreateClient();
    }

    [Fact]
    public async Task GetAllLocations_WhenLocationsPresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync("/location/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<LocationViewModel[]>());
    }

    [Fact]
    public async Task GetALocationById_WhenLocationPresent_ReturnsOk()
    {
        const int id = 1;
        var response = await _httpClient.GetAsync($"/location/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<LocationDetailViewModel>());

        Assert.Contains("1", value);
        Assert.Contains("Eiffel Tower", value);
    }

    [Fact]
    public async Task CreateALocation_WhenLocationDetailsValidAndPresent_ReturnsOk()
    {
        const string name = "Louvre";
        const string locationType = "Museum";
        const int stopId = 2;
        
        CreateLocationViewModel newLocation = new CreateLocationViewModel()
        {
            Name = name,
            StopId = stopId,
            DateArrived = DateTime.Now - TimeSpan.FromDays(5),
            LocationType = locationType
        };
        
        var response = await _httpClient.PostAsJsonAsync("/location/", newLocation);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task UpdateALocation_WhenNewLocationDetailsValidAndPresent_ReturnsOk()
    {
        const int id = 1;
        const string newName = "Notre Dame";
        const string newType = "Tourist Spot";

        UpdateLocationViewModel updateLocation = new UpdateLocationViewModel()
        {
            Id = id,
            Name = newName,
            LocationType = newType,
            DateArrived = DateTime.Now - TimeSpan.FromDays(6)
        };

        var response = await _httpClient.PatchAsJsonAsync($"/location/{id}", updateLocation);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteALocation_WhenLocationFoundThenDeleted_ReturnsOk()
    {
        const int id = 3;

        var response = await _httpClient.DeleteAsync($"/location/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
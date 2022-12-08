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
        var response = await _httpClient.GetAsync("/api/locations/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<LocationViewModel[]>());
    }

    [Fact]
    public async Task GetALocationById_WhenLocationPresent_ReturnsOk()
    {
        const int id = 1;
        var response = await _httpClient.GetAsync($"/api/locations/{id}");
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
        
        var response = await _httpClient.PostAsJsonAsync("/api/locations/", newLocation);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateALocation_WhenLocationDetailsInvalidData_ReturnsErrorMessage()
    {
        const string name = "";
        const string locationType = "";
        const int stopId = 2;
        
        CreateLocationViewModel newLocation = new CreateLocationViewModel()
        {
            Name = name,
            StopId = stopId,
            DateArrived = DateTime.Now - TimeSpan.FromDays(5),
            LocationType = locationType
        };
        
        var response = await _httpClient.PostAsJsonAsync("/api/locations/", newLocation);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("Name", "Name must be entered for this location");
        result.Errors.CheckIfErrorPresent("LocationType", "Location type chosen was invalid");
        
        _testOutputHelper.WriteLine(value);
    }

    [Fact]
    public async Task UpdateALocation_WhenNewLocationDetailsValidAndPresent_ReturnsOk()
    {
        const int id = 4;
        const string newName = "Grand Canyon";
        const string newType = "Tourist Spot";

        UpdateLocationViewModel updateLocation = new UpdateLocationViewModel()
        {
            Id = id,
            Name = newName,
            LocationType = newType,
            DateArrived = DateTime.Now - TimeSpan.FromDays(6)
        };

        var response = await _httpClient.PatchAsJsonAsync($"/api/locations/{id}", updateLocation);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task UpdateALocation_WhenNewLocationDetailsInvalid_ReturnsErrorMessage()
    {
        const int id = 4;
        const string newName = "";
        const string newType = "";

        UpdateLocationViewModel updateLocation = new UpdateLocationViewModel()
        {
            Id = id,
            Name = newName,
            LocationType = newType,
            DateArrived = DateTime.Now - TimeSpan.FromDays(6)
        };

        var response = await _httpClient.PatchAsJsonAsync($"/api/locations/{id}", updateLocation);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("Name", "Name must be between 4 and 100 characters in length");
        result.Errors.CheckIfErrorPresent("LocationType", "Location type must be between 4 and 100 characters in length");
        
        _testOutputHelper.WriteLine(value);
    }
    
    [Fact]
    public async Task UpdateALocation_WhenNewLocationDetailsNull_ReturnsErrorMessage()
    {
        const int id = 4;
        const string newName = null;
        const string newType = null;

        UpdateLocationViewModel updateLocation = new UpdateLocationViewModel()
        {
            Id = id,
            Name = newName,
            LocationType = newType,
            DateArrived = DateTime.Now - TimeSpan.FromDays(6)
        };

        var response = await _httpClient.PatchAsJsonAsync($"/api/locations/{id}", updateLocation);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("NoValue", "At least one value required");
        
        _testOutputHelper.WriteLine(value);
    }

    [Fact]
    public async Task DeleteALocation_WhenLocationFoundThenDeleted_ReturnsOk()
    {
        const int id = 3;

        var response = await _httpClient.DeleteAsync($"/api/locations/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
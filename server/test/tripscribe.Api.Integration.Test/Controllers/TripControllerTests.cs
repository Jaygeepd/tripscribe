using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using Tripscribe.Api.Integration.Test.Base;
using tripscribe.Api.Integration.Test.Models;
using tripscribe.Api.Integration.Test.TestUtilities;
using Tripscribe.Api.Integration.Test.TestUtilities;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Trips;
using tripscribe.Dal.Models;
using Xunit.Abstractions;

namespace tripscribe.Api.Integration.Test.Controllers;

[Collection("Integration")]
public class TripControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _testOutputHelper;

    public TripControllerTests(ITestOutputHelper testOutputHelper, IntegrationClassFixture integrationFixture)
    {
        _testOutputHelper = testOutputHelper;
        _httpClient = integrationFixture.Host.CreateClient();
    }

    [Fact]
    public async Task GetAllTrips_WhenTripsPresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync("/api/trips/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<TripViewModel[]>());
    }

    [Fact]
    public async Task GetATripById_WhenTripPresent_ReturnsOk()
    {
        const int id = 1;
        var response = await _httpClient.GetAsync($"/api/trips/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<TripDetailViewModel>());

        Assert.Contains("1", value);
        Assert.Contains("French Trip", value);
        Assert.Contains("Short adventure around Paris", value);
    }
    
    [Fact]
    public async Task GetATripById_TripDoesNotExist_ThrowsException()
    {
        const int id = 20;
        var response = await _httpClient.GetAsync($"/api/trips/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task GetAccountTripsById_WhenAccountTripsPresent_ReturnsOk()
    {
        const int id = 1;
        var response = await _httpClient.GetAsync($"/api/trips/{id}/accounts/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<AccountViewModel[]>());

        Assert.Contains("3", value);
        Assert.Contains("Alex", value);
        Assert.Contains("Bell", value);
        Assert.Contains("tester3@yahoo.com", value);
    }

    [Fact]
    public async Task CreateATrip_WhenTripDetailsValidAndPresent_ReturnsOk()
    {
        const string title = "Cork Campaign";
        const string description = "Escape to Munster";
        
        CreateTripViewModel newTrip = new CreateTripViewModel
        {
            Title = title,
            Description = description
        };
        
        var response = await _httpClient.PostAsJsonAsync("/api/trips/", newTrip);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task CreateATrip_WhenTripDetailsNull_ReturnErrorMessage()
    {
        const string title = null;
        
        CreateTripViewModel newTrip = new CreateTripViewModel
        {
            Title = title
        };
        
        var response = await _httpClient.PostAsJsonAsync("/api/trips/", newTrip);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("Title", "Title must be not null");
        
        _testOutputHelper.WriteLine(value);
    }

    [Fact]
    public async Task CreateATrip_WhenTripDetailsInvalid_ReturnErrorMessage()
    {
        const string title = "";
        
        CreateTripViewModel newTrip = new CreateTripViewModel
        {
            Title = title
        };
        
        var response = await _httpClient.PostAsJsonAsync("/api/trips/", newTrip);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("Title", "Title must be between 5 and 100 characters in length");
        
        _testOutputHelper.WriteLine(value);
    }

    [Fact]
    public async Task UpdateATrip_WhenNewTripDetailsValidAndPresent_ReturnsOk()
    {
        const int id = 4;
        const string newTitle = "German Tour";
        const string newDescription = "Drowning in Bratwurst and Beer";

        UpdateTripViewModel updateTrip = new UpdateTripViewModel()
        {
            Id = id,
            Title = newTitle,
            Description = newDescription
        };

        var response = await _httpClient.PatchAsJsonAsync($"/api/trips/{id}", updateTrip);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task UpdateATrip_WhenNewTripDetailsNull_ReturnsErrorMessage()
    {
        const int id = 4;
        const string newTitle = null;

        UpdateTripViewModel updateTrip = new UpdateTripViewModel()
        {
            Id = id,
            Title = newTitle
        };

        var response = await _httpClient.PatchAsJsonAsync($"/api/trips/{id}", updateTrip);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("Title", "Title must be not null");
        
        _testOutputHelper.WriteLine(value);
    }
    
    [Fact]
    public async Task UpdateATrip_WhenNewTripDetailsInvalid_ReturnsErrorMessage()
    {
        const int id = 4;
        const string newTitle = "Hell";

        UpdateTripViewModel updateTrip = new UpdateTripViewModel()
        {
            Id = id,
            Title = newTitle
        };

        var response = await _httpClient.PatchAsJsonAsync($"/api/trips/{id}", updateTrip);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("Title", "Title must be between 5 and 100 characters in length");
        
        _testOutputHelper.WriteLine(value);
    }
    
    [Fact]
    public async Task DeleteATrip_WhenTripFoundThenDeleted_ReturnsOk()
    {
        const int id = 3;

        var response = await _httpClient.DeleteAsync($"/api/trips/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
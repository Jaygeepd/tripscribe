using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using Tripscribe.Api.Integration.Test.Base;
using tripscribe.Api.Integration.Test.TestUtilities;
using Tripscribe.Api.Integration.Test.TestUtilities;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Dal.Models;
using Xunit.Abstractions;

namespace tripscribe.Api.Integration.Test.Controllers;

[Collection("Integration")]
public class JourneyControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _testOutputHelper;

    public JourneyControllerTests(ITestOutputHelper testOutputHelper, IntegrationClassFixture integrationFixture)
    {
        _testOutputHelper = testOutputHelper;
        _httpClient = integrationFixture.Host.CreateClient();
    }

    [Fact]
    public async Task GetAllJourneys_WhenJourneysPresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync("/journey/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<JourneyViewModel[]>());
    }

    [Fact]
    public async Task GetAJourneyById_WhenJourneyPresent_ReturnsOk()
    {
        const int id = 1;
        var response = await _httpClient.GetAsync($"/journey/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<JourneyDetailViewModel>());

        Assert.Contains("1", value);
        Assert.Contains("French Trip", value);
        Assert.Contains("Short adventure around Paris", value);
    }

    [Fact]
    public async Task GetAccountJourneysById_WhenAccountJourneysPresent_ReturnsOk()
    {
        const int id = 1;
        var response = await _httpClient.GetAsync($"/journey/{id}/accounts/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<AccountViewModel[]>());

        Assert.Contains("3", value);
        Assert.Contains("Alex", value);
        Assert.Contains("Bell", value);
        Assert.Contains("tester3@yahoo.com", value);
    }

    [Fact]
    public async Task CreateAJourney_WhenJourneyDetailsValidAndPresent_ReturnsOk()
    {
        const string title = "Cork Campaign";
        const string description = "Escape to Munster";
        
        CreateJourneyViewModel newJourney = new CreateJourneyViewModel
        {
            Title = title,
            Description = description
        };
        
        var response = await _httpClient.PostAsJsonAsync("/journey/", newJourney);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task UpdateAJourney_WhenNewJourneyDetailsValidAndPresent_ReturnsOk()
    {
        const int id = 1;
        const string newTitle = "German Tour";
        const string newDescription = "Drowning in Bratwurst and Beer";

        UpdateJourneyViewModel updateJourney = new UpdateJourneyViewModel()
        {
            Id = id,
            Title = newTitle,
            Description = newDescription
        };

        var response = await _httpClient.PatchAsJsonAsync($"/journey/{id}", updateJourney);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteAJourney_WhenJourneyFoundThenDeleted_ReturnsOk()
    {
        const int id = 3;

        var response = await _httpClient.DeleteAsync($"/journey/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
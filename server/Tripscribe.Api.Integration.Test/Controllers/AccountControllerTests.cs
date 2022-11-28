using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using Tripscribe.Api.Integration.Test.Base;
using Tripscribe.Api.Integration.Test.TestUtilities;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Dal.Models;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TotalArmyBuilder.Api.Integration.Test.Controllers;

[Collection("Integration")]
public class AccountControllerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _httpClient;
    
    public AccountControllerTests(ITestOutputHelper testOutputHelper, IntegrationClassFixture integrationFixture)
    {
        _testOutputHelper = testOutputHelper;
        _httpClient = integrationFixture.Host.CreateClient();
    } 
    
    [Fact]
    public async Task GetAllAccounts_WhenAccountsPresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync($"/account/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
            
        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<AccountViewModel[]>());
    }
    
    [Fact]
    public async Task GetAnAccountById_WhenAccountPresent_ReturnsOk()
    {
        const int id = 1;
        var response = await _httpClient.GetAsync($"/account/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<AccountDetailViewModel>());
        
        Assert.Contains("1", value);
        Assert.Contains("tester1@gmail.com", value);
        Assert.Contains("Joe", value);
        Assert.Contains("Bloggs", value);
    }
    
    [Fact]
    public async Task GetAccountJourneysById_WhenAccountJourneysPresent_ReturnsOk()
    {
        const int id = 1;
        var response = await _httpClient.GetAsync($"/account/{id}/journeys/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<JourneyViewModel[]>());
        
        Assert.Contains("1", value);
        Assert.Contains("French Trip", value);
        Assert.Contains("Short adventure around Paris", value);
    }
    
    [Fact]
    public async Task CreateAnAccount_WhenAccountDetails_ValidAndPresent_ReturnsOk()
    {
        const int id = 4;
        const string firstName = "Andrew";
        const string lastName = "Yates";
        const string email = "ayates@gmail.com";
        const string password = "p455word";

        Account newAccount = new Account
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        var accountJson = JsonConvert.SerializeObject(newAccount);

        var stringContent = new StringContent(accountJson);
        var response = await _httpClient.PostAsync($"/accounts/", stringContent);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task UpdateAnAccount_WhenNewAccountDetails_ValidAndPresent_ReturnsOk()
    {
        const int id = 1;
        const string newFirstName = "Chandler";

        Account account = new Account
        {
            FirstName = newFirstName
        };

        var accountJson = JsonConvert.SerializeObject(account);

        var stringContent = new StringContent(accountJson);
        var response = await _httpClient.PatchAsync($"/accounts/{id}", stringContent);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task DeleteAnAccount_WhenAccountFound_ThenDeleted_ReturnsOk()
    {
        const int id = 1;
        
        var response = await _httpClient.DeleteAsync($"/accounts/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
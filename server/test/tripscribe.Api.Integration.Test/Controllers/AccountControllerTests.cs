using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tripscribe.Api.Integration.Test.Base;
using tripscribe.Api.Integration.Test.Models;
using tripscribe.Api.Integration.Test.TestUtilities;
using Tripscribe.Api.Integration.Test.TestUtilities;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Dal.Models;
using Xunit.Abstractions;

namespace tripscribe.Api.Integration.Test.Controllers;

[Collection("Integration")]
public class AccountControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _testOutputHelper;

    public AccountControllerTests(ITestOutputHelper testOutputHelper, IntegrationClassFixture integrationFixture)
    {
        _testOutputHelper = testOutputHelper;
        _httpClient = integrationFixture.Host.CreateClient();
    }

    [Fact]
    public async Task GetAllAccounts_WhenAccountsPresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync("/account/");
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
    public async Task GetAnAccountById_AccountDoesNotExist_ThrowException()
    {
        const int id = 20;
        var response = await _httpClient.GetAsync($"/account/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

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
        Assert.Contains("Biking Across Japan", value);
        Assert.Contains("Chris Broad inspired week", value);
    }
    
    [Fact]
    public async Task GetAnAccountJourneysById_AccountDoesNotExist_ThrowException()
    {
        const int id = 20;
        var response = await _httpClient.GetAsync($"/account/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

    }

    [Fact]
    public async Task CreateAnAccount_WhenAccountDetailsValidAndPresent_ReturnsOk()
    {
        const string firstName = "Andrew";
        const string lastName = "Yates";
        const string email = "ayates@gmail.com";
        const string password = "p455word";
        
        CreateAccountViewModel newAccount = new CreateAccountViewModel
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        
        var response = await _httpClient.PostAsJsonAsync($"/account/", newAccount);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task CreateAnAccount_WhenAccountDetailsInvalid_ReturnsValidationError()
    {
        const string firstName = "";
        const string lastName = "";
        const string email = "";
        const string password = "";
        
        CreateAccountViewModel newAccount = new CreateAccountViewModel
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        
        var response = await _httpClient.PostAsJsonAsync("/account/", newAccount);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("Email", "'Email' is not a valid email address.");
        result.Errors.CheckIfErrorPresent("FirstName", "First name must be entered, and under 100 characters in length");
        result.Errors.CheckIfErrorPresent("LastName", "Last name must be entered, and under 100 characters in length");
        result.Errors.CheckIfErrorPresent("Password", "Password must be between 8 and 30 characters");
        
        _testOutputHelper.WriteLine(value);
    }

    [Fact]
    public async Task UpdateAnAccount_WhenNewAccountDetails_ValidAndPresent_ReturnsOk()
    {
        const int id = 4;
        const string newFirstName = "Chandler";
        const string newLastName = "Bing";

        UpdateAccountViewModel updateAccount = new UpdateAccountViewModel()
        {
            FirstName = newFirstName,
            LastName = newLastName
        };

        var response = await _httpClient.PatchAsJsonAsync($"/account/{id}", updateAccount);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task UpdateAnAccount_WhenNewAccountDetailsInvalid_ReturnsError()
    {
        const int id = 2;
        const string newFirstName = null;
        const string newLastName = null;

        UpdateAccountViewModel updateAccount = new UpdateAccountViewModel()
        {
            FirstName = newFirstName,
            LastName = newLastName
        };

        var response = await _httpClient.PatchAsJsonAsync($"/account/{id}", updateAccount);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        //  ((string)result.Errors.NoValue[0]).Should().Be("At least one value required");
        
        _testOutputHelper.WriteLine(value);
        
    }

    [Fact]
    public async Task DeleteAnAccount_WhenAccountFound_ThenDeleted_ReturnsOk()
    {
        const int id = 3;

        var response = await _httpClient.DeleteAsync($"/account/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

}
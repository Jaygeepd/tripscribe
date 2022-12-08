using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;
using tripscribe.Services.Exceptions;
using tripscribe.Services.Profiles;
using tripscribe.Services.Services;
using tripscribe.Services.Test.Extensions;

namespace tripscribe.Services.Test.Services;

public class AccountServiceTest
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    private readonly IFixture _fixture;

    public AccountServiceTest()
    {
        _database = Substitute.For<ITripscribeDatabase>();
        _mapper = GetMapper();
        _fixture = new Fixture();
        
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
    }

    [Fact]
    public void GetAccount_WhenAccountExist_ReturnsAccount()
    {
        // Arrange
        const int id = 1;

        var account = new Account { Id = id };
        
        var accounts = new List<Account> { account };
        
        _database.Get<Account>().Returns(accounts.AsQueryable());

        var service = RetrieveService();

        // Act
        var result = service.GetAccount(id);
        
        // Assert
        result.Should().BeEquivalentTo(account, options => options.ExcludingMissingMembers());

    }

    [Fact]
    public void GetAccounts_WhenAccountsExist_ReturnsAccountList()
    {
        // Arrange 
        var accountList = _fixture.Build<Account>()
            .Without(x => x.AccountJourneys)
            .Without(x => x.JourneyReviews)
            .Without(x => x.LocationReviews)
            .Without(x => x.StopReviews)
            .CreateMany();
        _database.Get<Account>().Returns(accountList.AsQueryable());

        var service = RetrieveService();
        
        // Act 
        var result = service.GetAccounts();
        
        // Assert
        result.Should().BeEquivalentTo(accountList, options => options.ExcludingMissingMembers());
    }

    [Fact]
    public void CreateAccount_ValidDataEntered_MapperAndSaved()
    {
        // Arrange 
        const int id = 1;
        const string email = "test@gmail.com";
        const string firstName = "Joe";
        const string lastName = "Bloggs";
        const string password = "passw0rd";

        var newAccount = new AccountDTO
        {
            Id = id,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Password = password
        };

        var service = RetrieveService();

        // Act
        service.CreateAccount(newAccount);
        
        // Assert
        _database.Received(1).SaveChanges();
        _database.Received(1).Add(Arg.Is<Account>(x => x.FirstName == newAccount.FirstName));
    }
    
    [Theory]
    [InlineData("firstName", "lastName")]
    [InlineData(null, null)]
    public void UpdateAccount_ValidDataEntered_MapperAndSaved(string firstName, string lastName)
    {
        // Arrange
        var accountList = _fixture.Build<Account>()
        .Without(x => x.AccountJourneys)
        .Without(x => x.JourneyReviews)
        .Without(x => x.LocationReviews)
        .Without(x => x.StopReviews)
        .CreateMany();
        var accountDTO = _mapper.Map<AccountDTO>(accountList.First());
        
        _database.Get<Account>().Returns(accountList.AsQueryable());
        
        var service = RetrieveService();

        var updateAccount = accountList.First(); 
        
        _database.When(x => x.SaveChanges()).Do(x =>
        {
            accountList.First().Should().BeEquivalentTo(accountDTO, o => o.ExcludingMissingMembers());
        });
        
        // Act
        service.UpdateAccount(updateAccount.Id, accountDTO);

        // Assert
        _database.Received(1).SaveChanges();
    }

    [Fact]
    public void UpdateAccount_AccountDoesNotExist_ThrowNotFoundException()
    {
        // Arrange 
        const int correctId = 1;
        const int incorrectId = 2;
        
        var account = _fixture.Build<Account>()
            .With(x => x.Id, correctId)
            .Create();

        var updateDTO = _fixture.Build<AccountDTO>()
            .Create();

        var service = RetrieveService();
        
        // Act/Assert
        Assert.Throws<NotFoundException>(() => service.UpdateAccount(incorrectId, updateDTO));

    }
    
    [Fact]
    public void DeleteAccount_ValidIdEntered_MapperAndSaved()
    {
        // Arrange
        var accountList = _fixture.Build<Account>()
            .Without(x => x.AccountJourneys)
            .Without(x => x.JourneyReviews)
            .Without(x => x.LocationReviews)
            .Without(x => x.StopReviews)
            .CreateMany();

        _database.Get<Account>().Returns(accountList.AsQueryable());
        
        var service = RetrieveService();

        // Act
        service.DeleteAccount(accountList.First().Id);

        // Assert
        _database.Received(1).Get<Account>();
        _database.Received(1).Delete(accountList.First());
        _database.Received(1).SaveChanges();
    }
    
    [Fact]
    public void DeleteAccount_AccountDoesNotExist_ThrowNotFoundException()
    {
        // Arrange 
        const int correctId = 1;
        const int incorrectId = 2;
        
        var account = _fixture.Build<Account>()
            .With(x => x.Id, correctId)
            .Create();

        var service = RetrieveService();
        
        // Act/Assert
        Assert.Throws<NotFoundException>(() => service.DeleteAccount(incorrectId));

    }

    [Fact]
    public void GetAccountJourneys_ValidIdEntered_ReturnedAndMapped()
    {
        // Arrange
        const int journeyId = 1;
        const int accountId = 1;
        
        var journey = _fixture
            .Build<Journey>()
            .With(x => x.Id, journeyId)
            .Without(x => x.AccountJourneys)
            .CreateMany(1)
            .ToArray();

        var  accountIds = _fixture.MockWithOne(accountId);
        
        var accountJourneyList = _fixture
            .Build<AccountJourney>()
            .With(x => x.AccountId, accountIds.GetValue)
            .With(x => x.JourneyId, journeyId)
            .With(x => x.Journey, journey.First())
            .CreateMany()
            .AsQueryable();

        _database.Get<AccountJourney>().Returns(accountJourneyList);
        
        var service = RetrieveService();

        // Act
        var result = service.GetAccountJourneys(accountId);

        // Assert
        result.Should().BeEquivalentTo(journey, options => options.ExcludingMissingMembers());
    }

    private IAccountService RetrieveService()
    {
        return new AccountService(_database, _mapper);
    }
    
    private static IMapper GetMapper()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<AccountProfile>();
            cfg.AddProfile<JourneyProfile>();
            cfg.AddProfile<StopProfile>();
            cfg.AddProfile<ReviewProfile>();
        });
        
        return new Mapper(config);
    }
}
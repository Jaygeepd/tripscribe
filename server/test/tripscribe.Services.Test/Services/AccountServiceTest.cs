using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;
using tripscribe.Services.Profiles;
using tripscribe.Services.Services;

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
        
        // Act
        service.UpdateAccount(updateAccount.Id, accountDTO);

        // Assert
        _database.Received(1).Get<Account>();
        _database.Received(1).SaveChanges();
        _database.Received(1).Add(Arg.Is<Account>(x => x.FirstName == updateAccount.FirstName));
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
        _database.Received(1).SaveChanges();
    }

    [Fact]
    public void GetAccountJourneys_ValidIdEntered_ReturnedAndMapped()
    {
        // Arrange
        var accountList = _fixture.Build<Account>()
            .Without(x => x.AccountJourneys)
            .Without(x => x.JourneyReviews)
            .Without(x => x.LocationReviews)
            .Without(x => x.StopReviews)
            .CreateMany();
        _database.Get<Account>().Returns(accountList.AsQueryable());

        var journeyList = _fixture.Build<Journey>()
            .Without(x => x.AccountJourneys)
            .Without(x => x.JourneyReviews)
            .CreateMany();
        _database.Get<Journey>().Returns(journeyList.AsQueryable());
        
        var service = RetrieveService();
        

        // Act
        var result = service.GetAccountJourneys(accountList.First().Id);

        // Assert
        result.Should().BeEquivalentTo(journeyList, options => options.ExcludingMissingMembers());
    }
    
    [Fact]
    public void GetAccountReviews_ValidIdEntered_ReturnedAndMapped()
    {
        // Arrange 
        var accountList = _fixture.Build<Account>()
            .Without(x => x.AccountJourneys)
            .Without(x => x.JourneyReviews)
            .Without(x => x.LocationReviews)
            .Without(x => x.StopReviews)
            .CreateMany();
        _database.Get<Account>().Returns(accountList.AsQueryable());
        
        var reviewList = _fixture.Build<Review>()
            .Without(x => x.LocationReviews)
            .Without(x => x.JourneyReviews)
            .Without(x => x.StopReviews)
            .CreateMany();
        _database.Get<Review>().Returns(reviewList.AsQueryable());

        var service = RetrieveService();
        
        // Act 
        var result = service.GetAccountReviews(accountList.First().Id);
        
        // Assert
        result.Should().BeEquivalentTo(reviewList, options => options.ExcludingMissingMembers());
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
        });
        
        return new Mapper(config);
    }
}
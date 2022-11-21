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

    private IAccountService RetrieveService()
    {
        return new AccountService(_database, _mapper);
    }
    
    private static IMapper GetMapper()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<AccountProfile>();
        });
        
        return new Mapper(config);
    }
}
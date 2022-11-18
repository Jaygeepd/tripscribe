using AutoMapper;
using FluentAssertions;
using NSubstitute;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Services.Profiles;
using tripscribe.Services.Services;

namespace tripscribe.Services.Test.Services;

public class AccountServiceTest
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;

    public AccountServiceTest()
    {
        _database = Substitute.For<ITripscribeDatabase>();
        _mapper = GetMapper();
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
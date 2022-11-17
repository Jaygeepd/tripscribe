using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using tripscribe.Api.Controllers;
using tripscribe.Api.Test.Extensions;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Services.DTOs;
using tripscribe.Services.Services;

namespace tripscribe.Api.Test.Controllers;

[ExcludeFromCodeCoverage]
public class AccountControllerTests
{
    private readonly IMapper _mapper;
    private readonly IAccountService _service;

    public AccountControllerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _service = Substitute.For<IAccountService>();
    }

    [Fact]
    public void GetAccount_WhenAccountFound_MapsAndReturned()
    {
        // Arrange
        const int id = 1;
        var account = new AccountDTO
        {
            Id = id
        };

        var accountViewModel = new AccountViewModel();

        _service.GetAccount(id).Returns(account);
        _mapper.Map<AccountViewModel>(account).Returns(accountViewModel);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetAccount(id);
        
        // Assert
        var result = actionResult.AssertObjectResult<AccountViewModel, OkObjectResult>();

        result.Should().BeSameAs(accountViewModel);

        _service.Received(1).GetAccount(id);
        _mapper.Received(1).Map<AccountViewModel>(account);
    }

    [Theory]
    [InlineData("email", "firstname", "lastname")]
    [InlineData(null, null, null)]
    public void GetAccounts_WhenAccountsFound_MappedAndReturned(string email, string firstName, string lastName)
    {
        // Arrange
        const int id1 = 1;
        const int id2 = 2;
        
        var account1 = new AccountDTO()
        {
            Id = id1,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };
        
        var account2 = new AccountDTO()
        {
            Id = id2,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };

        var accountList = new List<AccountDTO>
        {
            account1, account2
        };

        var accountViewModels = new List<AccountViewModel>();

        _service.GetAccounts(email, firstName, lastName).Returns(accountList);
        _mapper.Map<IList<AccountViewModel>>(accountList).Returns(accountViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetAccounts(email, firstName, lastName);
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<AccountViewModel>, OkObjectResult>();

        result.Should().BeSameAs(accountViewModels);

        _service.Received(1).GetAccounts(email, firstName, lastName);
        _mapper.Received(1).Map<IList<AccountViewModel>>(accountList);

    }

    private AccountController RetrieveController()
    {
        return new AccountController(_mapper, _service);
    }
}
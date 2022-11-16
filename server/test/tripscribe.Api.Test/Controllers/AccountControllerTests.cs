using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using tripscribe.Api.Controllers;
using tripscribe.Api.Test.Extensions;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;
using tripscribe.Services.Services;

namespace tripscribe.Api.Test.Controllers;

public class AccountControllerTests
{
    private readonly IMapper _mapper;
    private readonly IAccountService _service;
    private readonly ITripscribeDatabase _database;

    public AccountControllerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _service = Substitute.For<IAccountService>();
        _database = Substitute.For<ITripscribeDatabase>();
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

    [Fact]
    public void GetJourney_WhenJourneyFound_MapsAndReturned()
    {
        // Arrange
        const int id = 1;
    }

    private AccountController RetrieveController()
    {
        return new AccountController(_database, _mapper, _service);
    }
}
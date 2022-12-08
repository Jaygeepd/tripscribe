using System.Diagnostics.CodeAnalysis;
using System.Net;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using tripscribe.Api.Controllers;
using tripscribe.Api.Test.Extensions;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Api.ViewModels.Reviews;
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
    
    [Theory]
    [InlineData("email", "firstname", "lastname", "password")]
    public void CreateAccount_WhenValidDataEntered_MappedAndSaved(string email, string firstName, string lastName, string password)
    {
        // Arrange
        var account = new AccountDTO
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Password = password
        };

        var createAccountViewModel = new CreateAccountViewModel();

        _mapper.Map<AccountDTO>(createAccountViewModel).Returns(account);

        var controller = RetrieveController();

        // Act
        var actionResult = controller.CreateAccount(createAccountViewModel);

        // Assert
        actionResult.AssertResult<StatusCodeResult>(HttpStatusCode.Created);

        _service.Received(1).CreateAccount(account);
        _mapper.Received(1).Map<AccountDTO>(createAccountViewModel);
    }

    [Fact]
    public void DeleteAccount_WhenCalledWithValidId_DeletedAndSaved()
    {
        // Arrange
        const int id = 1;

        var controller = RetrieveController();

        // Act
        var actionResult = controller.DeleteAccount(id);

        // Assert
        actionResult.AssertResult<NoContentResult>();

        _service.Received(1).DeleteAccount(id);
    }
    
    [Fact]
    public void GetAccountReviews_WhenReviewsFound_MapsAndReturned()
    {
        // Arrange
        const int searchId = 1;

        const int id1 = 1;
        const int id2 = 2;
        
        var review1 = new ReviewDTO
        {
            Id = id1
        };

        var review2 = new ReviewDTO
        {
            Id = id2
        };

        var reviewList = new List<ReviewDTO>
        {
            review1, review2
        };
        
        var reviewViewModels = new List<ReviewViewModel>();

        _service.GetAccountReviews(searchId).Returns(reviewList);
        _mapper.Map<IList<ReviewViewModel>>(reviewList).Returns(reviewViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetAccountReviews(searchId);
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<ReviewViewModel>, OkObjectResult>();

        result.Should().BeSameAs(reviewViewModels);

        _service.Received(1).GetAccountReviews(searchId);
        _mapper.Received(1).Map<IList<ReviewViewModel>>(reviewList);
    }
    
    [Fact]
    public void GetAccountJourneys_WhenAccountJourneysFound_MapsAndReturned()
    {
        // Arrange
        const int searchId = 1;

        const int id1 = 1;
        const int id2 = 2;
        
        var journey1 = new JourneyDTO
        {
            Id = id1
        };

        var journey2 = new JourneyDTO()
        {
            Id = id2
        };

        var journeyList = new List<JourneyDTO>
        {
            journey1, journey2
        };
        
        var journeyViewModels = new List<JourneyViewModel>();

        _service.GetAccountJourneys(searchId).Returns(journeyList);
        _mapper.Map<IList<JourneyViewModel>>(journeyList).Returns(journeyViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetAccountJourneys(searchId);
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<JourneyViewModel>, OkObjectResult>();

        result.Should().BeSameAs(journeyViewModels);

        _service.Received(1).GetAccountJourneys(searchId);
        _mapper.Received(1).Map<IList<JourneyViewModel>>(journeyList);
    }

    private AccountsController RetrieveController()
    {
        return new AccountsController(_mapper, _service);
    }
}
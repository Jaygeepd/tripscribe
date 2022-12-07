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
public class JourneyControllerTests
{
    private readonly IMapper _mapper;
    private readonly IJourneyService _service;

    public JourneyControllerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _service = Substitute.For<IJourneyService>();
    }

    [Fact]
    public void GetJourney_WhenJourneyFound_MapsAndReturned()
    {
        // Arrange
        const int id = 1;
        var journey = new JourneyDTO
        {
            Id = id
        };

        var journeyViewModel = new JourneyViewModel();

        _service.GetJourney(id).Returns(journey);
        _mapper.Map<JourneyViewModel>(journey).Returns(journeyViewModel);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetJourney(id);
        
        // Assert
        var result = actionResult.AssertObjectResult<JourneyViewModel, OkObjectResult>();

        result.Should().BeSameAs(journeyViewModel);

        _service.Received(1).GetJourney(id);
        _mapper.Received(1).Map<JourneyViewModel>(journey);
    }
    
    [Theory]
    [InlineData("title", "description", "2017-7-10", "2017-7-10", "2017-7-10")]
    [InlineData(null, null, null, null, null)]
    public void GetJourneys_WhenJourneysFound_MappedAndReturned(string title, string description, DateTime timestamp, 
        DateTime startTime, DateTime endTime)
    {
        // Arrange
        const int id1 = 1;
        const int id2 = 2;
        
        var journey1 = new JourneyDTO()
        {
            Id = id1,
            Title = title,
            Description = description,
            Timestamp = timestamp
        };
        
        var journey2 = new JourneyDTO()
        {
            Id = id2,
            Title = title,
            Description = description,
            Timestamp = timestamp
        };

        var journeyList = new List<JourneyDTO>
        {
            journey1, journey2
        };

        var journeyViewModels = new List<JourneyViewModel>();

        _service.GetJourneys(title, startTime, endTime).Returns(journeyList);
        _mapper.Map<IList<JourneyViewModel>>(journeyList).Returns(journeyViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetJourneys(title, startTime, endTime);
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<JourneyViewModel>, OkObjectResult>();

        result.Should().BeSameAs(journeyViewModels);

        _service.Received(1).GetJourneys(title, startTime, endTime);
        _mapper.Received(1).Map<IList<JourneyViewModel>>(journeyList);

    }
    
    [Theory]
    [InlineData("title", "description")]
    public void CreateJourney_WhenValidDataEntered_MappedAndSaved(string title, string description)
    {
        // Arrange
        var journey = new JourneyDTO
        {
            Title = title,
            Description = description
        };

        var createJourneyViewModel = new CreateJourneyViewModel();

        _mapper.Map<JourneyDTO>(createJourneyViewModel).Returns(journey);

        var controller = RetrieveController();

        // Act
        var actionResult = controller.CreateJourney(createJourneyViewModel);

        // Assert
        actionResult.AssertResult<StatusCodeResult>(HttpStatusCode.Created);

        _service.Received(1).CreateJourney(journey);
        _mapper.Received(1).Map<JourneyDTO>(createJourneyViewModel);
    }
    
    [Theory]
    [InlineData("title", "description")]
    [InlineData(null, null)]
    public void UpdateJourney_WhenCalledWithValidViewModel_MappedAndSaved(string title, string description)
    {
        // Arrange
        const int id = 1;
        var journey = new JourneyDTO()
        {
            Id = id,
            Title = title,
            Description = description,
        };

        var updateJourneyViewModel = new UpdateJourneyViewModel();

        _mapper.Map<JourneyDTO>(updateJourneyViewModel).Returns(journey);

        var controller = RetrieveController();

        // Act
        var actionResult = controller.UpdateJourney(id, updateJourneyViewModel);

        // Assert
        actionResult.AssertResult<NoContentResult>();

        _service.Received(1).UpdateJourney(id, journey);
        _mapper.Received(1).Map<JourneyDTO>(updateJourneyViewModel);
    }
    
    [Fact]
    public void DeleteJourney_WhenCalledWithValidId_DeletedAndSaved()
    {
        // Arrange
        const int id = 1;

        var controller = RetrieveController();

        // Act
        var actionResult = controller.DeleteJourney(id);

        // Assert
        actionResult.AssertResult<NoContentResult>();

        _service.Received(1).DeleteJourney(id);
    }
    
    [Fact]
    public void GetJourneyReviews_WhenReviewsFound_MapsAndReturned()
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

        _service.GetJourneyReviews(searchId).Returns(reviewList);
        _mapper.Map<IList<ReviewViewModel>>(reviewList).Returns(reviewViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetJourneyReviews(searchId);
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<ReviewViewModel>, OkObjectResult>();

        result.Should().BeSameAs(reviewViewModels);

        _service.Received(1).GetJourneyReviews(searchId);
        _mapper.Received(1).Map<IList<ReviewViewModel>>(reviewList);
    }
    
    [Fact]
    public void GetJourneyAccounts_WhenAccountJourneysFound_MapsAndReturned()
    {
        // Arrange
        const int searchId = 1;

        const int id1 = 1;
        const int id2 = 2;
        
        var account1 = new AccountDTO
        {
            Id = id1
        };

        var account2 = new AccountDTO()
        {
            Id = id2
        };

        var accountList = new List<AccountDTO>
        {
            account1, account2
        };
        
        var accountViewModels = new List<AccountViewModel>();

        _service.GetJourneyAccounts(searchId).Returns(accountList);
        _mapper.Map<IList<AccountViewModel>>(accountList).Returns(accountViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetJourneyAccounts(searchId);
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<AccountViewModel>, OkObjectResult>();

        result.Should().BeSameAs(accountViewModels);

        _service.Received(1).GetJourneyAccounts(searchId);
        _mapper.Received(1).Map<IList<AccountViewModel>>(accountList);
    }
    
    private JourneysController RetrieveController()
    {
        return new JourneysController(_mapper, _service);
    }
}
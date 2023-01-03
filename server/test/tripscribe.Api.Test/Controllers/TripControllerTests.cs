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
public class TripControllerTests
{
    private readonly IMapper _mapper;
    private readonly ITripService _service;

    public TripControllerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _service = Substitute.For<ITripService>();
    }

    [Fact]
    public void GetTrip_WhenTripFound_MapsAndReturned()
    {
        // Arrange
        const int id = 1;
        var trip = new TripDTO
        {
            Id = id
        };

        var tripViewModel = new TripViewModel();

        _service.GetTrip(id).Returns(trip);
        _mapper.Map<TripViewModel>(trip).Returns(tripViewModel);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetTrip(id);
        
        // Assert
        var result = actionResult.AssertObjectResult<TripViewModel, OkObjectResult>();

        result.Should().BeSameAs(tripViewModel);

        _service.Received(1).GetTrip(id);
        _mapper.Received(1).Map<TripViewModel>(trip);
    }
    
    [Theory]
    [InlineData("title", "description", "2017-7-10", "2017-7-10", "2017-7-10")]
    [InlineData(null, null, null, null, null)]
    public void GetTrips_WhenTripsFound_MappedAndReturned(string title, string description, DateTime timestamp, 
        DateTime startTime, DateTime endTime)
    {
        // Arrange
        const int id1 = 1;
        const int id2 = 2;
        
        var trip1 = new TripDTO()
        {
            Id = id1,
            Title = title,
            Description = description,
            Timestamp = timestamp
        };
        
        var trip2 = new TripDTO()
        {
            Id = id2,
            Title = title,
            Description = description,
            Timestamp = timestamp
        };

        var tripList = new List<TripDTO>
        {
            trip1, trip2
        };

        var tripViewModels = new List<TripViewModel>();

        _service.GetTrips(title, startTime, endTime).Returns(tripList);
        _mapper.Map<IList<TripViewModel>>(tripList).Returns(tripViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetTrips(title, startTime, endTime);
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<TripViewModel>, OkObjectResult>();

        result.Should().BeSameAs(tripViewModels);

        _service.Received(1).GetTrips(title, startTime, endTime);
        _mapper.Received(1).Map<IList<TripViewModel>>(tripList);

    }
    
    [Theory]
    [InlineData("title", "description")]
    public void CreateTrip_WhenValidDataEntered_MappedAndSaved(string title, string description)
    {
        // Arrange
        var trip = new TripDTO
        {
            Title = title,
            Description = description
        };

        var createTripViewModel = new CreateTripViewModel();

        _mapper.Map<TripDTO>(createTripViewModel).Returns(trip);

        var controller = RetrieveController();

        // Act
        var actionResult = controller.CreateTrip(createTripViewModel);

        // Assert
        actionResult.AssertResult<StatusCodeResult>(HttpStatusCode.Created);

        _service.Received(1).CreateTrip(trip);
        _mapper.Received(1).Map<TripDTO>(createTripViewModel);
    }
    
    [Theory]
    [InlineData("title", "description")]
    [InlineData(null, null)]
    public void UpdateTrip_WhenCalledWithValidViewModel_MappedAndSaved(string title, string description)
    {
        // Arrange
        const int id = 1;
        var trip = new TripDTO()
        {
            Id = id,
            Title = title,
            Description = description,
        };

        var updateTripViewModel = new UpdateTripViewModel();

        _mapper.Map<TripDTO>(updateTripViewModel).Returns(trip);

        var controller = RetrieveController();

        // Act
        var actionResult = controller.UpdateTrip(id, updateTripViewModel);

        // Assert
        actionResult.AssertResult<NoContentResult>();

        _service.Received(1).UpdateTrip(id, trip);
        _mapper.Received(1).Map<TripDTO>(updateTripViewModel);
    }
    
    [Fact]
    public void DeleteJourney_WhenCalledWithValidId_DeletedAndSaved()
    {
        // Arrange
        const int id = 1;

        var controller = RetrieveController();

        // Act
        var actionResult = controller.DeleteTrip(id);

        // Assert
        actionResult.AssertResult<NoContentResult>();

        _service.Received(1).DeleteTrip(id);
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

        _service.GetTripReviews(searchId).Returns(reviewList);
        _mapper.Map<IList<ReviewViewModel>>(reviewList).Returns(reviewViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetTripReviews(searchId);
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<ReviewViewModel>, OkObjectResult>();

        result.Should().BeSameAs(reviewViewModels);

        _service.Received(1).GetTripReviews(searchId);
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

        _service.GetTripAccounts(searchId).Returns(accountList);
        _mapper.Map<IList<AccountViewModel>>(accountList).Returns(accountViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetTripAccounts(searchId);
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<AccountViewModel>, OkObjectResult>();

        result.Should().BeSameAs(accountViewModels);

        _service.Received(1).GetTripAccounts(searchId);
        _mapper.Received(1).Map<IList<AccountViewModel>>(accountList);
    }
    
    private TripsController RetrieveController()
    {
        return new TripsController(_mapper, _service);
    }
}
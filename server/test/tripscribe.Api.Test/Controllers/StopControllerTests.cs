using System.Diagnostics.CodeAnalysis;
using System.Net;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using tripscribe.Api.Controllers;
using tripscribe.Api.Test.Extensions;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Api.ViewModels.Stop;
using tripscribe.Services.DTOs;
using tripscribe.Services.Services;

namespace tripscribe.Api.Test.Controllers;

[ExcludeFromCodeCoverage]
public class StopControllerTests
{
    private readonly IMapper _mapper;
    private readonly IStopService _service;

    public StopControllerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _service = Substitute.For<IStopService>();
    }
    
    [Fact]
    public void GetStop_WhenStopFound_MapsAndReturned()
    {
        // Arrange
        const int id = 1;
        var stop = new StopDTO
        {
            Id = id
        };

        var stopViewModel = new StopViewModel();

        _service.GetStop(id).Returns(stop);
        _mapper.Map<StopViewModel>(stop).Returns(stopViewModel);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetStop(id);
        
        // Assert
        var result = actionResult.AssertObjectResult<StopViewModel, OkObjectResult>();

        result.Should().BeSameAs(stopViewModel);

        _service.Received(1).GetStop(id);
        _mapper.Received(1).Map<StopViewModel>(stop);
    }
    
    [Theory]
    [InlineData("name", "2017-7-10", "2017-7-10", "2017-7-10", "2017-7-10", 
        "2017-7-10", "2017-7-10", 1)]
    [InlineData(null, null, null, null, null, null, null, null)]
    public void GetStops_WhenStopsFound_MappedAndReturned(string name, DateTime dateArrived, DateTime dateDeparted, 
        DateTime arrivedStartDate, DateTime arrivedEndDate, 
        DateTime departedStartDate, DateTime departedEndDate, int journeyId)
    {
        // Arrange
        const int id1 = 1;
        const int id2 = 2;
        
        var stop1 = new StopDTO()
        {
            Id = id1,
            Name = name,
            DateArrived = dateArrived,
            DateDeparted = dateDeparted,
            JourneyId = journeyId
        };
        
        var stop2 = new StopDTO()
        {
            Id = id2,
            Name = name,
            DateArrived = dateArrived,
            DateDeparted = dateDeparted,
            JourneyId = journeyId
        };

        var stopList = new List<StopDTO>
        {
            stop1, stop2
        };

        var stopViewModels = new List<StopViewModel>();

        _service.GetStops(name, arrivedStartDate, arrivedEndDate, departedStartDate, 
            departedEndDate, journeyId).Returns(stopList);
        _mapper.Map<IList<StopViewModel>>(stopList).Returns(stopViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetStops(name, arrivedStartDate, arrivedEndDate, departedStartDate, 
            departedEndDate, journeyId);
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<StopViewModel>, OkObjectResult>();

        result.Should().BeSameAs(stopViewModels);

        _service.Received(1).GetStops(name, arrivedStartDate, arrivedEndDate, departedStartDate, 
            departedEndDate, journeyId);
        _mapper.Received(1).Map<IList<StopViewModel>>(stopList);

    }
    
    [Theory]
    [InlineData("name", "2017-7-10", "2017-7-10", 1)]
    public void CreateStop_WhenValidDataEntered_MappedAndSaved(string name, DateTime dateArrived, DateTime dateDeparted, int journeyId)
    {
        // Arrange
        var stop = new StopDTO
        {
            Name = name,
            DateArrived = dateArrived,
            DateDeparted = dateDeparted,
            JourneyId = journeyId
        };

        var createStopViewModel = new CreateStopViewModel();

        _mapper.Map<StopDTO>(createStopViewModel).Returns(stop);

        var controller = RetrieveController();

        // Act
        var actionResult = controller.CreateStop(createStopViewModel);

        // Assert
        actionResult.AssertResult<StatusCodeResult>(HttpStatusCode.Created);

        _service.Received(1).CreateStop(stop);
        _mapper.Received(1).Map<StopDTO>(createStopViewModel);
    }
    
    [Theory]
    [InlineData("name", "2017-7-10", "2017-7-10")]
    [InlineData(null, null, null)]
    public void UpdateStop_WhenCalledWithValidViewModel_MappedAndSaved(string name, DateTime dateArrived, DateTime dateDeparted)
    {
        // Arrange
        const int id = 1;
        var stop = new StopDTO
        {
            Id = id,
            DateArrived = dateArrived,
            DateDeparted = dateDeparted,
        };

        var updateStopViewModel = new UpdateStopViewModel();

        _mapper.Map<StopDTO>(updateStopViewModel).Returns(stop);

        var controller = RetrieveController();

        // Act
        var actionResult = controller.UpdateStop(id, updateStopViewModel);

        // Assert
        actionResult.AssertResult<NoContentResult>();

        _service.Received(1).UpdateStop(id, stop);
        _mapper.Received(1).Map<StopDTO>(updateStopViewModel);
    }
    
    [Fact]
    public void DeleteStop_WhenCalledWithValidId_DeletedAndSaved()
    {
        // Arrange
        const int id = 1;

        var controller = RetrieveController();

        // Act
        var actionResult = controller.DeleteStop(id);

        // Assert
        actionResult.AssertResult<NoContentResult>();

        _service.Received(1).DeleteStop(id);
    }

    [Fact]
    public void GetStopReviews_WhenReviewsFound_MapsAndReturned()
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

        _service.GetStopReviews(searchId).Returns(reviewList);
        _mapper.Map<IList<ReviewViewModel>>(reviewList).Returns(reviewViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetStopReviews(searchId);
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<ReviewViewModel>, OkObjectResult>();

        result.Should().BeSameAs(reviewViewModels);

        _service.Received(1).GetStopReviews(searchId);
        _mapper.Received(1).Map<IList<ReviewViewModel>>(reviewList);
    }
    
    private StopController RetrieveController()
    {
        return new StopController(_mapper, _service);
    }
}
using System.Diagnostics.CodeAnalysis;
using System.Net;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using tripscribe.Api.Controllers;
using tripscribe.Api.Test.Extensions;
using tripscribe.Api.ViewModels.Locations;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Services.DTOs;
using tripscribe.Services.Services;

namespace tripscribe.Api.Test.Controllers;

[ExcludeFromCodeCoverage]
public class LocationControllerTests
{
    private readonly IMapper _mapper;
    private readonly ILocationService _service;

    public LocationControllerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _service = Substitute.For<ILocationService>();
    }

    [Fact]
    public void GetLocation_WhenLocationFound_MapsAndReturned()
    {
        // Arrange
        const int id = 1;
        var location = new LocationDTO()
        {
            Id = id
        };

        var locationViewModel = new LocationViewModel();

        _service.GetLocation(id).Returns(location);
        _mapper.Map<LocationViewModel>(location).Returns(locationViewModel);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetLocation(id);
        
        // Assert
        var result = actionResult.AssertObjectResult<LocationViewModel, OkObjectResult>();

        result.Should().BeSameAs(locationViewModel);

        _service.Received(1).GetLocation(id);
        _mapper.Received(1).Map<LocationViewModel>(location);
    }

    [Theory]
    [InlineData("name", "2017-7-10", "locationtype", 1, "2016-02-12", "2018-02-12")]
    [InlineData(null, null, null, null, null, null)]
    public void GetLocations_WhenLocationsFound_MappedAndReturned(string name, DateTime dateArrived, string locationType, int stopId, DateTime startDate, DateTime endDate)
    {
        // Arrange
        const int id1 = 1;
        const int id2 = 2;
        
        var location1 = new LocationDTO()
        {
            Id = id1,
            Name = name,
            DateVisited = dateArrived,
            LocationType = locationType
        };
        
        var location2 = new LocationDTO()
        {
            Id = id2,
            Name = name,
            DateVisited = dateArrived,
            LocationType = locationType
        };

        var locationList = new List<LocationDTO>
        {
            location1, location2
        };

        var locationViewModels = new List<LocationViewModel>();

        _service.GetLocations(name, locationType, startDate, endDate, stopId).Returns(locationList);
        _mapper.Map<IList<LocationViewModel>>(locationList).Returns(locationViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetLocations(name, locationType, startDate, endDate, stopId);
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<LocationViewModel>, OkObjectResult>();

        result.Should().BeSameAs(locationViewModels);

        _service.Received(1).GetLocations(name, locationType, startDate, endDate, stopId);
        _mapper.Received(1).Map<IList<LocationViewModel>>(locationList);

    }
    
    [Theory]
    [InlineData("name", "2017-7-10", "locationtype", 1)]
    public void CreateLocation_WhenValidDataEntered_MappedAndSaved(string name, DateTime dateArrived, string locationType, int stopId)
    {
        // Arrange
        var location = new LocationDTO
        {
            Name = name,
            DateVisited = dateArrived,
            LocationType = locationType,
            StopId = stopId
        };

        var createLocationViewModel = new CreateLocationViewModel();

        _mapper.Map<LocationDTO>(createLocationViewModel).Returns(location);

        var controller = RetrieveController();

        // Act
        var actionResult = controller.CreateLocation(createLocationViewModel);

        // Assert
        actionResult.AssertResult<StatusCodeResult>(HttpStatusCode.Created);

        _service.Received(1).CreateLocation(location);
        _mapper.Received(1).Map<LocationDTO>(createLocationViewModel);
    }
    
    [Theory]
    [InlineData("name", "2017-7-10", "locationtype")]
    [InlineData(null, null, null)]
    public void UpdateLocation_WhenCalledWithValidViewModel_MappedAndSaved(string name, DateTime dateArrived, string locationType)
    {
        // Arrange
        const int id = 1;
        var location = new LocationDTO
        {
            Id = id,
            Name = name,
            DateVisited = dateArrived,
            LocationType = locationType
        };

        var updateLocationViewModel = new UpdateLocationViewModel();

        _mapper.Map<LocationDTO>(updateLocationViewModel).Returns(location);

        var controller = RetrieveController();

        // Act
        var actionResult = controller.UpdateLocation(id, updateLocationViewModel);

        // Assert
        actionResult.AssertResult<NoContentResult>();

        _service.Received(1).UpdateLocation(id, location);
        _mapper.Received(1).Map<LocationDTO>(updateLocationViewModel);
    }
    
    [Fact]
    public void DeleteLocation_WhenCalledWithValidId_DeletedAndSaved()
    {
        // Arrange
        const int id = 1;

        var controller = RetrieveController();

        // Act
        var actionResult = controller.DeleteLocation(id);

        // Assert
        actionResult.AssertResult<NoContentResult>();

        _service.Received(1).DeleteLocation(id);
    }
    
    [Fact]
    public void GetLocationReviews_WhenReviewsFound_MapsAndReturned()
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

        _service.GetLocationReviews(searchId).Returns(reviewList);
        _mapper.Map<IList<ReviewViewModel>>(reviewList).Returns(reviewViewModels);

        var controller = RetrieveController();
        
        // Act
        var actionResult = controller.GetLocationReviews(searchId);
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<ReviewViewModel>, OkObjectResult>();

        result.Should().BeSameAs(reviewViewModels);

        _service.Received(1).GetLocationReviews(searchId);
        _mapper.Received(1).Map<IList<ReviewViewModel>>(reviewList);
    }

    private LocationsController RetrieveController()
    {
        return new LocationsController(_mapper, _service);
    }
}
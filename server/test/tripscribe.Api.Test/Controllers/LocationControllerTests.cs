using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using tripscribe.Api.Controllers;
using tripscribe.Api.Test.Extensions;
using tripscribe.Api.ViewModels.Locations;
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
    [InlineData("name", "datearrived", "locationtype", 1, "startdate", "enddate")]
    [InlineData(null, null, null, null, null, null)]
    public void GetAccounts_WhenAccountsFound_MappedAndReturned(string name, DateTime dateArrived, string locationType, int stopId, DateTime startDate, DateTime endDate)
    {
        // Arrange
        const int id1 = 1;
        const int id2 = 2;
        
        var location1 = new LocationDTO()
        {
            Id = id1,
            Name = name,
            DateArrived = dateArrived,
            LocationType = locationType
        };
        
        var location2 = new LocationDTO()
        {
            Id = id2,
            Name = name,
            DateArrived = dateArrived,
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
        _mapper.Received(1).Map<IList<LocationViewModel>>(locationViewModels);

    }

    private LocationController RetrieveController()
    {
        return new LocationController(_mapper, _service);
    }
}
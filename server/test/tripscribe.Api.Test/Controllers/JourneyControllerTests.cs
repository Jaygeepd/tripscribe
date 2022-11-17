using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using tripscribe.Api.Controllers;
using tripscribe.Api.Test.Extensions;
using tripscribe.Api.ViewModels.Journeys;
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
    [InlineData("title", "description", "Timestamp", "starttime", "endtime")]
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
    
    private JourneyController RetrieveController()
    {
        return new JourneyController(_mapper, _service);
    }
}
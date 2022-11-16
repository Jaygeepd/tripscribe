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
    
    private JourneyController RetrieveController()
    {
        return new JourneyController(_mapper, _service);
    }
}
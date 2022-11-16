using System.Net;
using AutoMapper;
using FluentAssertions.Equivalency.Tracing;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Services.DTOs;
using tripscribe.Services.Services;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class JourneyController : ControllerBase
{
    private readonly IJourneyService _service;
    private readonly IMapper _mapper;
    
    public JourneyController(IMapper mapper, IJourneyService service) => 
        (_mapper, _service) = (mapper, service);
    
    [HttpGet]
    public ActionResult<JourneyViewModel> GetJourney([FromQuery] int id)
    {
        var journey = _service.GetJourney(id);
        
        return Ok(_mapper.Map<JourneyViewModel>(journey));
    }
    
    [HttpGet]
    public ActionResult<IList<JourneyViewModel>> GetJourneys([FromQuery] string title, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
    {
        var journeys = _service.GetJourneys(title, startTime, endTime);
        
        return Ok(_mapper.Map<JourneyViewModel>(journeys));
    }

    [HttpGet(template:"{id}/accounts", Name = "GetJourneyAccounts")]
    public ActionResult<AccountViewModel> GetJourneyAccounts(int id)
    {

        var accounts = _service
            .GetJourneyAccounts(id);
        return Ok(_mapper.Map<IList<AccountViewModel>>(accounts));
    }
    
    [HttpGet("{id}/reviews", Name = "GetJourneyReviews")]
    public ActionResult<IList<ReviewViewModel>> GetJourneyReviews(int id)
    {
        var reviews = _service
            .GetJourneyReviews(id);

        return Ok(_mapper.Map<IList<ReviewViewModel>>(reviews));
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public ActionResult CreateJourney( [FromBody] CreateJourneyViewModel journeyDetails)
    {
        var newJourney = new CreateJourneyViewModel
        {
            Title = journeyDetails.Title,
            Description = journeyDetails.Description, 
            Timestamp = DateTime.Now
        };

        var journey = _mapper.Map<JourneyDTO>(newJourney);
        
        _service.CreateJourney(journey);
        
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateJourney(int id, [FromBody] UpdateJourneyViewModel journeyDetails)
    {
        var journey = _mapper.Map<JourneyDTO>(journeyDetails);

        _service.UpdateJourney(id, journey);
        
        return StatusCode((int)HttpStatusCode.NoContent);
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult DeleteJourney(int id)
    {
        _service.DeleteJourney(id);

        return StatusCode((int)HttpStatusCode.NoContent);
    }
}
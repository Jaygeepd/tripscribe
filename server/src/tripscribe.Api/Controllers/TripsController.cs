using System.Net;
using AutoMapper;
using FluentAssertions.Equivalency.Tracing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Api.ViewModels.Stop;
using tripscribe.Api.ViewModels.Trips;
using tripscribe.Services.DTOs;
using tripscribe.Services.Services;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripService _service;
    private readonly IMapper _mapper;
    
    public TripsController(IMapper mapper, ITripService service) => 
        (_mapper, _service) = (mapper, service);
    
    [HttpGet("{id}")]
    public ActionResult<TripDetailViewModel> GetTrip(int id)
    {
        var trip = _service.GetTrip(id);
        
        return Ok(_mapper.Map<TripDetailViewModel>(trip));
    }
    
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<IList<TripViewModel>> GetTrips([FromQuery] string? title, [FromQuery] DateTime? startTime, [FromQuery] DateTime? endTime)
    {
        var trips = _service.GetTrips(title, startTime, endTime);
        
        return Ok(_mapper.Map<IList<TripDetailViewModel>>(trips));
    }

    [HttpGet(template:"{id}/accounts", Name = "GetTripAccounts")]
    public ActionResult<IList<AccountViewModel>> GetTripAccounts(int id)
    {

        var accounts = _service
            .GetTripAccounts(id);
        return Ok(_mapper.Map<IList<AccountViewModel>>(accounts));
    }
    
    [HttpGet(template:"{id}/stops", Name = "GetTripStops")]
    public ActionResult<IList<StopViewModel>> GetTripStops(int id)
    {

        var stops = _service
            .GetTripStops(id);
        return Ok(_mapper.Map<IList<StopViewModel>>(stops));
    }
    
    [HttpGet("{id}/reviews", Name = "GetTripReviews")]
    public ActionResult<IList<ReviewViewModel>> GetTripReviews(int id)
    {
        var reviews = _service
            .GetTripReviews(id);

        return Ok(_mapper.Map<IList<ReviewViewModel>>(reviews));
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public ActionResult CreateTrip( [FromBody] CreateTripViewModel tripDetails)
    {
        
        var trip = _mapper.Map<TripDTO>(tripDetails);
        
        _service.CreateTrip(trip);
        
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPatch]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateTrip(int id, [FromBody] UpdateTripViewModel updateDetails)
    {
        var existingTrip = _service.GetTrip(id);

        _mapper.Map(updateDetails, existingTrip);

        _service.UpdateTrip(id, existingTrip);
        
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult DeleteTrip(int id)
    {
        _service.DeleteTrip(id);

        return NoContent();
    }
}
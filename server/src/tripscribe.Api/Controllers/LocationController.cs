using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.ViewModels.Locations;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Services.DTOs;
using tripscribe.Services.Services;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _service;
    private readonly IMapper _mapper;
    public LocationController(IMapper mapper, ILocationService service) => 
        (_mapper, _service) = (mapper, service);
    
    [HttpGet]
    public ActionResult<LocationViewModel> GetLocations([FromQuery] string name, [FromQuery] string locationType, 
        [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int stopId)
    {
        var locations = _service.GetLocations(name, locationType, startDate, endDate, stopId);
        return Ok(_mapper.Map<IList<LocationViewModel>>(locations));
    }
    
    [HttpGet("{id}", Name = "GetLocation")]
    public ActionResult<LocationDetailViewModel> GetLocation(int id)
    {
        var location = _service.GetLocation(id);

        return Ok(_mapper.Map<IList<LocationViewModel>>(location));
    }
    
    [HttpGet("{id}/reviews", Name = "GetReviewsByLocationId")]
    public ActionResult<ReviewViewModel> GetLocationReviews(int id)
    {
        var reviews = _service.GetLocationReviews(id);

        return Ok(_mapper.Map<ReviewViewModel>(reviews));
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public ActionResult CreateLocation( [FromBody] CreateLocationViewModel locationDetails)
    {
        var newLocation = new CreateLocationViewModel()
        {
            Name = locationDetails.Name,
            DateArrived = locationDetails.DateArrived, 
            LocationType = locationDetails.LocationType,
            StopId = locationDetails.StopId
        };

        var location = _mapper.Map<LocationDTO>(newLocation);
        
        _service.CreateLocation(location);
        
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateLocation(int id, [FromBody] UpdateLocationViewModel locationDetails)
    {
        var location = _mapper.Map<LocationDTO>(locationDetails);

        _service.UpdateLocation(id, location);
        
        return StatusCode((int)HttpStatusCode.NoContent);
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult DeleteLocation(int id)
    {
        _service.DeleteLocation(id);

        return StatusCode((int)HttpStatusCode.NoContent);
    }
}
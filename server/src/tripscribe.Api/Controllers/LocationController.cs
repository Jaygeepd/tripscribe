using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.ViewModels.Locations;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.Locations;
using tripscribe.Dal.Specifications.Reviews;
using tripscribe.Services.Services;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _service;
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    public LocationController(ITripscribeDatabase database, IMapper mapper, ILocationService service) => 
        (_database, _mapper, _service) = (database, mapper, service);
    
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
        
        if (location == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<IList<LocationViewModel>>(location));
    }
    
    [HttpGet("{id}/reviews", Name = "GetReviewsByLocationId")]
    public ActionResult<ReviewViewModel> GetLocationReviews(int id)
    {
        var reviews = _database
            .Get<LocationReview>()
            .Where(new LocationReviewsByLocationIdSpec(id))
            .Select(x => x.Review.ReviewText)
            .ToList();

        return Ok(reviews);
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public ActionResult CreateLocation( [FromBody] CreateLocationViewModel locationDetails)
    {
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateLocation(int id, [FromBody] UpdateLocationViewModel locationDetails)
    {
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateLocation(int id)
    {
        return NoContent();
    }
}
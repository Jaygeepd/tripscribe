using System.Net;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.testDI;
using tripscribe.Api.ViewModels.Locations;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.Locations;
using tripscribe.Dal.Specifications.Reviews;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly ITripscribeDatabase _database;
    public LocationController(ITripscribeDatabase database) => _database = database;
    
    [HttpGet("stop/{id}", Name = "GetLocationsByStopId")]
    public ActionResult<LocationViewModel> GetLocationsByStopId(int id)
    {
        var stops = _database.Get<Location>()
            .Where(new LocationsByStopIdSpec(id))
            .ToList();
        return Ok(new { stops });
    }
    
    [HttpGet("{id}", Name = "GetLocation")]
    public ActionResult<LocationDetailViewModel> GetLocation(int id)
    {
        var location = _database
            .Get<Location>()
            .FirstOrDefault(new LocationByIdSpec(id));
        if (location == null)
        {
            return NotFound();
        }

        return Ok(new
        { Id = location.Id, Name = location.Name, DateVisited = location.DateVisited, 
            LocationType = location.LocationType, StopId = location.StopId });
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
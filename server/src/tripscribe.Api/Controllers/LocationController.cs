using System.Net;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.testDI;
using tripscribe.Api.ViewModels.Locations;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;

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
        var stops = _database.Get<Location>().Where(search => search.StopId.Equals(id)).ToList();
        return Ok(new { stops });
    }
    
    [HttpGet("{id}", Name = "GetLocation")]
    public ActionResult<LocationDetailViewModel> GetLocation(int id)
    {
        var location = _database.Get<Location>().FirstOrDefault(x => x.Id == id);
        if (location == null)
        {
            return NotFound();
        }

        return Ok(new
        { Id = location.Id, Name = location.Name, DateVisited = location.DateVisited, 
            LocationType = location.LocationType, StopId = location.StopId });
    }
    
    [HttpGet("{id}/reviews", Name = "GetReviewsByLocationId")]
    public ActionResult<ReviewViewModel> GetReviewsByLocationId()
    {
        var reviews = new List<ReviewViewModel>
        {
            new ReviewViewModel()
            {
                ReviewText = "Pass"
            }
        };
        
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
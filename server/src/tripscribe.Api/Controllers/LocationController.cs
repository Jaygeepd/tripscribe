using System.Net;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.testDI;
using tripscribe.Api.ViewModels.Locations;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly IDoStuff _doStuff;
    public LocationController(IDoStuff doStuff)
    {
        _doStuff = doStuff;
    }
    
    [HttpGet]
    public ActionResult<LocationViewModel> GetLocations()
    {
        var stuff = _doStuff.Stuff();
        var locations = new List<LocationViewModel>
        {
            new LocationViewModel()
            {
                Name = stuff
            }
        };
        
        return Ok(locations);
    }
    
    [HttpGet("{id}", Name = "GetLocation")]
    public ActionResult<LocationDetailViewModel> GetLocation(int id)
    {
        return Ok(new { Amount = 108, Message = "Hello" });
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
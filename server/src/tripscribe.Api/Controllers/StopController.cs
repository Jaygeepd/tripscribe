using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tripscribe.Api.testDI;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Api.ViewModels.Stop;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StopController : ControllerBase
{
    private readonly ITripscribeDatabase _database;
    public StopController(ITripscribeDatabase database) => _database = database;
    
    [HttpGet]
    public ActionResult<StopViewModel> GetAccounts()
    {
        var accounts = _database.Get<Stop>().ToList();
        return Ok(new { accounts });
    }
     
    [HttpGet("journey/{id}", Name = "GetStopsByJourneyId")]
    public ActionResult<StopViewModel> GetStopsByJourneyId(int id)
    {
        var stops = _database
            .Get<Stop>()
            .Where(search => search.JourneyId.Equals(id))
            .ToList();
        return Ok(new { stops });
    }
    
    [HttpGet("{Id}/reviews", Name = "GetReviewsByStopId")]
    public ActionResult<ReviewViewModel> GetReviewsByStopId()
    {
        var reviews = new List<ReviewViewModel>
        {
            new ReviewViewModel()
            {
                ReviewText = "pass"
            }
        };
        
        return Ok(reviews);
    }
    
    [HttpGet("{id}", Name = "GetStop")]
    public ActionResult<StopDetailViewModel> GetStop(int id)
    {
        var stop = _database
            .Get<Stop>()
            .Select(x => new { Id = x.Id, Name = x.Name, 
                DateArrived = x.DateArrived, DateDeparted = x.DateDeparted,
                Locations = x.Locations.Select(y => y.Name)})
            .FirstOrDefault(x => x.Id == id);
        if (stop == null)
        {
            return NotFound();
        }

        return Ok(new
            { Id = stop.Id, Name = stop.Name, DateArrived = stop.DateArrived, DateDeparted = stop.DateDeparted, 
                Locations = stop.Locations });
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public ActionResult CreateStop( [FromBody] CreateStopViewModel stopDetails)
    {
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateStop(int id, [FromBody] UpdateStopViewModel stopDetails)
    {
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateStop(int id)
    {
        return NoContent();
    }
}
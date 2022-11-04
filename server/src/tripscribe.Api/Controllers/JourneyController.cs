using System.Net;
using FluentAssertions.Equivalency.Tracing;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.testDI;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class JourneyController : ControllerBase
{
    private readonly ITripscribeDatabase _database;
    
    public JourneyController(ITripscribeDatabase database) => _database = database;
    
    [HttpGet]
    public ActionResult<JourneyViewModel> GetJourneys()
    {
        var journeys = _database.Get<Journey>().ToList();
        return Ok(journeys);
    }
    
    [HttpGet("{id}", Name = "GetJourney")]
    public ActionResult<JourneyDetailViewModel> GetJourney(int id)
    {
        var journey = _database.Get<Journey>().FirstOrDefault(x => x.Id == id);
        if (journey == null)
        {
            return NotFound();
        }
        return Ok(new { Id = journey.Id, Title = journey.Title, Description = journey.Description, Timestamp = journey.Timestamp});
    }

    [HttpGet(template:"{id}/accounts", Name = "GetJourneyAccounts")]
    public ActionResult<AccountViewModel> GetJourneyAccounts()
    {
        
        var accounts = _database.Get<Account>().ToList();
        return Ok(accounts);
    }
    
    [HttpGet("{id}/reviews", Name = "GetJourneyReviews")]
    public ActionResult<ReviewViewModel> GetJourneyReviews(int id)
    {
        var reviews = _database.Get<Review>().ToList();

        return Ok(reviews);
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public ActionResult CreateJourney( [FromBody] CreateJourneyViewModel journeyDetails)
    {
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateJourney(int id, [FromBody] UpdateJourneyViewModel journeyDetails)
    {
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateJourney(int id)
    {
        return NoContent();
    }
}
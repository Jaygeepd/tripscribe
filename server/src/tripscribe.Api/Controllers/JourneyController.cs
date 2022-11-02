using System.Net;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.testDI;
using tripscribe.Api.ViewModels.Journeys;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class JourneyController : ControllerBase
{
    private readonly IDoStuff _doStuff;
    public JourneyController(IDoStuff doStuff)
    {
        _doStuff = doStuff;
    }
    
    [HttpGet]
    public ActionResult<JourneyViewModel> GetJourneys()
    {
        var stuff = _doStuff.Stuff();
        var journeys = new List<JourneyViewModel>
        {
            new JourneyViewModel()
            {
                Title = stuff
            }
        };
        
        return Ok(journeys);
    }
    
    [HttpGet("{id}", Name = "GetJourney")]
    public ActionResult<JourneyDetailViewModel> GetJourney(int id)
    {
        return Ok(new { Amount = 108, Message = "Hello" });
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
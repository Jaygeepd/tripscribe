using System.Net;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.testDI;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Api.ViewModels.Stop;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StopController : ControllerBase
{
    private readonly IDoStuff _doStuff;
    public StopController(IDoStuff doStuff)
    {
        _doStuff = doStuff;
    }
     
    [HttpGet("{journeyId}", Name = "GetStopsByJourneyId")]
    public ActionResult<StopViewModel> GetStopsByJourneyId()
    {
        var stuff = _doStuff.Stuff();
        var stops = new List<StopViewModel>
        {
            new StopViewModel()
            {
                Name = stuff
            }
        };
        
        return Ok(stops);
    }
    
    [HttpGet("{Id}/reviews", Name = "GetReviewsByStopId")]
    public ActionResult<ReviewViewModel> GetReviewsByStopId()
    {
        var stuff = _doStuff.Stuff();
        var reviews = new List<ReviewViewModel>
        {
            new ReviewViewModel()
            {
                ReviewText = stuff
            }
        };
        
        return Ok(reviews);
    }
    
    [HttpGet("{id}", Name = "GetStop")]
    public ActionResult<StopDetailViewModel> GetStop(int id)
    {
        return Ok(new { Amount = 108, Message = "Hello" });
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
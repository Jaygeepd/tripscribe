using System.Net;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.ViewModels.Reviews;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ReviewsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public ActionResult CreateReview( [FromBody] CreateReviewViewModel reviewDetails)
    {
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateReview(int id, [FromBody] UpdateReviewViewModel reviewDetails)
    {
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateReview(int id)
    {
        return NoContent();
    }
}
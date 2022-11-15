using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Api.ViewModels.Stop;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.Reviews;
using tripscribe.Dal.Specifications.Stops;
using tripscribe.Services.Services;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StopController : ControllerBase
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    private readonly IStopService _service;
    public StopController(ITripscribeDatabase database, IMapper mapper, IStopService service) =>
        (_database, _mapper, _service) = (database, mapper, service);
    
    [HttpGet]
    public ActionResult<StopViewModel> GetStops([FromQuery] string name, DateTime arrivedStartDate, DateTime arrivedEndDate, DateTime departedStartDate, DateTime departedEndDate, int journeyId)
    {
        var stops = _service.GetStops(name, arrivedStartDate, arrivedEndDate, 
            departedStartDate, departedEndDate, journeyId);
        return Ok(_mapper.Map<IList<StopViewModel>>(stops));
    }
    
    [HttpGet("{Id}/reviews", Name = "GetReviewsByStopId")]
    public ActionResult<ReviewViewModel> GetReviewsByStopId(int id)
    {
        
        var reviews = _database
            .Get<StopReview>()
            .Where(new StopReviewsByStopIdSpec(id))
            .Select(x => x.Review.ReviewText)
            .ToList();

        return Ok(reviews);
        
    }
    
    [HttpGet("{id}", Name = "GetStop")]
    public ActionResult<StopDetailViewModel> GetStop(int id)
    {
        var stop = _database
            .Get<Stop>()
            
            .Select(x => new
            {
                Id = x.Id, Name = x.Name,
                DateArrived = x.DateArrived, DateDeparted = x.DateDeparted,
                Locations = x.Locations.Select(y => y.Name)
            })
            .FirstOrDefault(x => x.Id  == id);
        
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
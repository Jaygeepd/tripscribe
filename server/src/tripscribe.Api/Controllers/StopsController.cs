using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Api.ViewModels.Stop;
using tripscribe.Services.DTOs;
using tripscribe.Services.Services;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StopsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IStopService _service;
    public StopsController(IMapper mapper, IStopService service) =>
        (_mapper, _service) = (mapper, service);
    
    [HttpGet("{id}", Name = "GetStopById")]
    public ActionResult<StopDetailViewModel> GetStop(int id)
    {
        var stop = _service.GetStop(id);
        return Ok(_mapper.Map<StopDetailViewModel>(stop));
    }
    
    [HttpGet]
    public ActionResult<IList<StopViewModel>> GetStops([FromQuery] string? name, DateTime? arrivedStartDate, DateTime? arrivedEndDate, DateTime? departedStartDate, DateTime? departedEndDate, int? journeyId)
    {
        var stops = _service.GetStops(name, arrivedStartDate, arrivedEndDate, 
            departedStartDate, departedEndDate, journeyId);
        return Ok(_mapper.Map<IList<StopViewModel>>(stops));
    }
    
    [HttpGet("{Id}/reviews", Name = "GetReviewsByStopId")]
    public ActionResult<IList<ReviewViewModel>> GetStopReviews(int id)
    {

        var reviews = _service.GetStopReviews(id);

        return Ok(_mapper.Map<IList<ReviewViewModel>>(reviews));
        
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public ActionResult CreateStop( [FromBody] CreateStopViewModel stopDetails)
    {
        var newStop = new CreateStopViewModel
        {
            Name = stopDetails.Name,
            DateArrived = stopDetails.DateArrived,
            DateDeparted = stopDetails.DateDeparted,
            TripId = stopDetails.TripId
        };

        var stop = _mapper.Map<StopDTO>(newStop);
        
        _service.CreateStop(stop);
        
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPatch]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateStop(int id, [FromBody] UpdateStopViewModel stopDetails)
    {
        var stop = _mapper.Map<StopDTO>(stopDetails);

        _service.UpdateStop(id, stop);
        
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult DeleteStop(int id)
    {
        _service.DeleteStop(id);

        return NoContent();
    }
}
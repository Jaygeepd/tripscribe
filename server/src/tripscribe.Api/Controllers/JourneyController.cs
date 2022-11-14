using System.Net;
using AutoMapper;
using FluentAssertions.Equivalency.Tracing;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.AccountJourneys;
using tripscribe.Dal.Specifications.Journeys;
using tripscribe.Dal.Specifications.Reviews;
using tripscribe.Services.Services;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class JourneyController : ControllerBase
{
    private readonly ITripscribeDatabase _database;
    private readonly IJourneyService _service;
    private readonly IMapper _mapper;
    
    public JourneyController(ITripscribeDatabase database, IJourneyService service, IMapper mapper) => 
        (_database, _service, _mapper) = (database, service, mapper);
    
    [HttpGet]
    public ActionResult<IList<JourneyViewModel>> GetJourneys([FromQuery] int id, [FromQuery] string title, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
    {
        var journeyViewModels = _service.GetJourneys(id, title, startTime, endTime);
        
        return Ok(journeyViewModels);
    }

    [HttpGet(template:"{id}/accounts", Name = "GetJourneyAccounts")]
    public ActionResult<AccountViewModel> GetJourneyAccounts(int id)
    {
        
        var accounts = _database
            .Get<AccountJourney>()
            .Where(new AccountJourneysByJourneyIdSpec(id))
            .Select(x => x.Account.FirstName)
            .ToList();
        return Ok(new { accounts });
    }
    
    [HttpGet("{id}/reviews", Name = "GetJourneyReviews")]
    public ActionResult<ReviewViewModel> GetJourneyReviews(int id)
    {
        var reviews = _database
            .Get<JourneyReview>()
            .Where(new JourneyReviewsByJourneyIdSpec(id))
            .Select(x => x.Review.ReviewText)
            .ToList();

        return Ok(reviews);
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public ActionResult CreateJourney( [FromBody] CreateJourneyViewModel journeyDetails)
    {
        var newJourney = new Journey
        {
            Title = journeyDetails.Title,
            Description = journeyDetails.Description, 
            Timestamp = DateTime.Now
        };

        _database.Add(newJourney);
        
        _database.SaveChanges();
        
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateJourney(int id, [FromBody] UpdateJourneyViewModel journeyDetails)
    {
        var updateJour = new Journey
        {
            Title = journeyDetails.Title,
            Description = journeyDetails.Description
        };

        var currJour = _database
            .Get<Journey>()
            .FirstOrDefault(x => x.Id == id);

        currJour.Title = updateJour.Title;
        currJour.Description = updateJour.Description;

        _database.SaveChanges();
        
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
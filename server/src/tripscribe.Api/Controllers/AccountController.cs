using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tripscribe.Api.testDI;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.AccountJourneys;
using tripscribe.Dal.Specifications.Accounts;
using tripscribe.Dal.Specifications.Reviews;
using tripscribe.Services.DTOs;
using tripscribe.Services.Services;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    private readonly IAccountService _service;
    public AccountController(ITripscribeDatabase database, IMapper mapper, IAccountService service) => 
        (_database, _mapper, _service) = (database, mapper, service);
    
    [HttpGet]
    public ActionResult<IList<AccountViewModel>> GetAccounts([FromQuery] string email, [FromQuery] string firstName, [FromQuery] string lastName)
    {
        var accounts = _service.GetAccounts(email, firstName, lastName);

        return Ok(_mapper.Map<IList<AccountViewModel>>(accounts));
    }

    [HttpGet("{id}", Name = "GetAccount")]
    public ActionResult<AccountDetailViewModel> GetAccount(int id)
    {
        var account = _database
            .Get<Account>()
            .FirstOrDefault(new AccountByIdSpec(id));
        
        if (account == null)
        {
            return NotFound();
        }

        return Ok(new
            { Id = account.Id, FirstName = account.FirstName, LastName = account.LastName, Email = account.Email });
    }

    [HttpGet("{id}/reviews", Name = "GetAccountReviews")]
    public ActionResult<ReviewViewModel> GetAccountReviews(int id)
    {
        var journeyReviews = _database
            .Get<JourneyReview>()
            .Where(new JourneyReviewsByAccountIdSpec(id))
            .Select(x => x.Review.ReviewText)
            .ToList();

        var stopReviews = _database
            .Get<StopReview>()
            .Where(new StopReviewsByAccountIdSpec(id))
            .Select(x => x.Review.ReviewText)
            .ToList();

        var locationReviews = _database
            .Get<LocationReview>()
            .Where(new LocationReviewsByAccountIdSpec(id))
            .Select(x => x.Review.ReviewText)
            .ToList();

        var reviews = journeyReviews.Concat(stopReviews).ToList();
        reviews = reviews.Concat(locationReviews).ToList();
        
        return Ok(new { reviews });
    }
    
    [HttpGet("{id}/journeys", Name = "GetAccountJourneys")]
    public ActionResult<JourneyViewModel> GetAccountJourneys(int id)
    {
        
        var journeys = _database
            .Get<AccountJourney>()
            .Where(new AccountJourneysByAccountIdSpec(id))
            .Select(x => x.Journey.Title)
            .ToList();
        return Ok(new { journeys });
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public ActionResult CreateAccount( [FromBody] CreateAccountViewModel accountDetails)
    {

        var newAccount = new Account
        {
            FirstName = accountDetails.FirstName,
            LastName = accountDetails.LastName, 
            Email = accountDetails.Email,
            Password = accountDetails.Password
        };

        _database.Add(newAccount);
        
        _database.SaveChanges();
        
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateAccount(int id, [FromBody] UpdateAccountViewModel updateDetails)
    {
        var account = _mapper.Map<AccountDTO>(updateDetails);

        _service.UpdateAccount(id, account);
        
        return StatusCode((int)HttpStatusCode.NoContent);
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult DeleteAccount(int id, [FromBody] UpdateAccountViewModel updateDetails)
    {

        return NoContent();
    }
}

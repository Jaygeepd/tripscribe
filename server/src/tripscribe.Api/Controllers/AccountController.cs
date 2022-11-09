using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tripscribe.Api.testDI;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.Accounts;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ITripscribeDatabase _database;
    public AccountController(ITripscribeDatabase database) => _database = database;
    
    [HttpGet]
    public ActionResult<AccountViewModel> GetAccounts()
    {
        var accounts = _database.Get<Account>().ToList();
        return Ok(new { accounts });
    }

    [HttpGet("{id}", Name = "GetAccount")]
    public ActionResult<AccountDetailViewModel> GetAccount(int id)
    {
        var account = _database
            .Get<Account>()
            .FirstOrDefault(new AccountByIdSpec(id)
                .And (new AccountByEmailSpec("HelloThere@email.com")));
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
        var reviews = _database
            .Get<Review>()
            .Where(x => x.Id.Equals(id))
            .ToList();
        return Ok(new { reviews });
    }
    
    [HttpGet("{id}/journeys", Name = "GetAccountJourneys")]
    public ActionResult<JourneyViewModel> GetAccountJourneys(int id)
    {
        
        var journeys = _database
            .Get<AccountJourney>()
            .Where(x => x.AccountId.Equals(id))
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
        var updateAcc = new Account
        {
            FirstName = updateDetails.FirstName,
            LastName = updateDetails.LastName
        };
        
        var currentAcc = _database
            .Get<Account>()
            .FirstOrDefault(x => x.Id.Equals(id));

        if (currentAcc == null)
        {
            return NotFound();
        }
        
        currentAcc.FirstName = updateAcc.FirstName;
        currentAcc.LastName = updateAcc.LastName;

        _database.SaveChanges();

        return StatusCode((int)HttpStatusCode.Accepted);
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult DeleteAccount(int id, [FromBody] UpdateAccountViewModel updateDetails)
    {

        return NoContent();
    }
}

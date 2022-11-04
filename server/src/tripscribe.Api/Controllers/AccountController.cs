using System.Net;
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
        var account = _database.Get<Account>().FirstOrDefault(x => x.Id == id);
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
        
        var reviews = _database.Get<Review>().ToList();
        return Ok(new { reviews });
    }
    
    [HttpGet("{id}/journeys", Name = "GetAccountJourneys")]
    public ActionResult<JourneyViewModel> GetAccountJourneys(int id)
    {
        
        var journeys = _database.Get<Journey>().ToList();
        return Ok(new { journeys });
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public ActionResult CreateAccount( [FromBody] CreateAccountViewModel accountDetails)
    {
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateAccount(int id, [FromBody] UpdateAccountViewModel accountDetails)
    {
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateAccount(int id)
    {
        return NoContent();
    }
}

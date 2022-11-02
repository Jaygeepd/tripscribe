using System.Net;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.testDI;
using tripscribe.Api.ViewModels.Accounts;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IDoStuff _doStuff;
    public AccountController(IDoStuff doStuff)
    {
        _doStuff = doStuff;
    }
    
    [HttpGet]
    public ActionResult<AccountViewModel> GetAccounts()
    {
        var stuff = _doStuff.Stuff();
        var accounts = new List<AccountViewModel>
        {
            new AccountViewModel
            {
                FirstName = stuff
            }
        };
        
        return Ok(accounts);
    }
    
    [HttpGet("{id}", Name = "GetAccount")]
    public ActionResult<AccountDetailViewModel> GetAccount(int id)
    {
        return Ok(new { Amount = 108, Message = "Hello" });
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

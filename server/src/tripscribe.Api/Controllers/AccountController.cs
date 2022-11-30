using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Services.DTOs;
using tripscribe.Services.Services;

namespace tripscribe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAccountService _service;
    public AccountController(IMapper mapper, IAccountService service) => 
        (_mapper, _service) = (mapper, service);
    
    [HttpGet("{id}", Name = "GetAccountsById")]
    public ActionResult<AccountViewModel> GetAccount(int id)
    {
        var accounts = _service.GetAccount(id);

        return Ok(_mapper.Map<AccountViewModel>(accounts));
    }
    
    [HttpGet]
    public ActionResult<IList<AccountViewModel>> GetAccounts([FromQuery] string? email, [FromQuery] string? firstName, 
        [FromQuery] string? lastName)
    {
        var accounts = _service.GetAccounts(email, firstName, lastName);

        return Ok(_mapper.Map<IList<AccountViewModel>>(accounts));
    }

    [HttpGet("{id}/reviews", Name = "GetAccountReviews")]
    public ActionResult<IList<ReviewViewModel>> GetAccountReviews(int id)
    {
        var reviews = _service.GetAccountReviews(id);

        return Ok(_mapper.Map<IList<ReviewViewModel>>(reviews));
    }
    
    [HttpGet("{id}/journeys", Name = "GetAccountJourneys")]
    public ActionResult<IList<JourneyViewModel>> GetAccountJourneys(int id)
    {

        var journeys = _service
            .GetAccountJourneys(id);
        
        return Ok(_mapper.Map<IList<JourneyViewModel>>(journeys));
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public ActionResult CreateAccount( [FromBody] CreateAccountViewModel accountDetails)
    {

        var account = _mapper.Map<AccountDTO>(accountDetails);
        
        _service.CreateAccount(account);
        
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPatch]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult UpdateAccount(int id, [FromBody] UpdateAccountViewModel updateDetails)
    {
        var existingAcc = _service.GetAccount(id);

        _mapper.Map(updateDetails, existingAcc);

        _service.UpdateAccount(id, existingAcc);
        
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public ActionResult DeleteAccount(int id)
    {
        _service.DeleteAccount(id);

        return NoContent();
    }
}

using System.Security.Claims;
using tripscribe.Services.DTOs;
using tripscribe.Services.Services;

namespace Tripscribe.Api.Authentication;

public class AuthorizedAccountProvider : IAuthorizedAccountProvider
{
    private AccountDTO? _account;
    private readonly IAccountService _accountService;
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthorizedAccountProvider(IAccountService accountService, IHttpContextAccessor contextAccessor)
    {
        _accountService = accountService;
        _contextAccessor = contextAccessor;
    }
    
    public AccountDTO GetLoggedInAccount()
    {
        if (_account is not null) return _account;

        var identifier = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(identifier)) return null;

        _account = _accountService.GetAccount(int.Parse(identifier));

        return _account;
    }
}

public interface IAuthorizedAccountProvider
{
    AccountDTO GetLoggedInAccount();
}
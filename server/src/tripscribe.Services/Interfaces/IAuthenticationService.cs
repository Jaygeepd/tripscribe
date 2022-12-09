using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface IAuthenticateService
{
    AccountDTO? Authenticate(string email, string password);
}
using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface IAccountService
{
    IList<AccountDTO> GetAccounts();
}
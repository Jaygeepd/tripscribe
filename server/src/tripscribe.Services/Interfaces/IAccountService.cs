using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface IAccountService
{
    IList<AccountDTO> GetAccount(int id);

    IList<AccountDTO> GetAccounts(string? email = null, string? firstName = null, string? lastName = null);
    void UpdateAccount(int id, AccountDTO account);

}
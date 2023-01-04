using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface IAccountService
{
    AccountDTO GetAccount(int id);

    IList<AccountDTO> GetAccounts(string? email = null, string? firstName = null, string? lastName = null);

    void CreateAccount(AccountDTO account);
    
    void UpdateAccount(int id, AccountDTO account);

    void DeleteAccount(int id);

    IList<ReviewDTO> GetAccountReviews(int id);

    IList<TripDTO> GetAccountTrips(int id);
}
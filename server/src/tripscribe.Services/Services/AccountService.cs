using AutoMapper;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.AccountJourneys;
using tripscribe.Dal.Specifications.Accounts;
using tripscribe.Dal.Specifications.Reviews;
using tripscribe.Services.DTOs;
using tripscribe.Services.Exceptions;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Services.Services;

public class AccountService : IAccountService
{
    private readonly ITripscribeDatabase _database; 
    private readonly IMapper _mapper;
    public AccountService(ITripscribeDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public AccountDTO GetAccount(int id)
    {
        var accountQuery = _database
            .Get<Account>()
            .Where(new AccountByIdSpec(id));

        return _mapper
            .ProjectTo<AccountDTO>(accountQuery)
            .SingleOrDefault();
    }

    public IList<AccountDTO> GetAccounts(string? email = null, string? firstName = null, string? lastName = null)
    {
        var accountQuery = _database
            .Get<Account>()
            .Where(new AccountSearchSpec(email, firstName, lastName));

        return _mapper
            .ProjectTo<AccountDTO>(accountQuery)
            .ToList();
        
    }

    public void CreateAccount(AccountDTO account)
    {
        var newAccount = _mapper.Map<Account>(account);

        newAccount.Password = BCrypt.Net.BCrypt.HashPassword(newAccount.Password);
        
        _database.Add(newAccount);
        _database.SaveChanges();
    }
    
    public void UpdateAccount(int id, AccountDTO account)
    {

        var currentAcc = _database
            .Get<Account>()
            .FirstOrDefault(new AccountByIdSpec(id));

        if (currentAcc == null) throw new NotFoundException("Account Not Found");

        if (account.Password is not null)
        {
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
        } 
        
        _mapper.Map(account, currentAcc);

        _database.SaveChanges();
    }

    public void DeleteAccount(int id)
    {
        var currentAcc = _database
            .Get<Account>()
            .FirstOrDefault(new AccountByIdSpec(id));

        if (currentAcc == null) throw new NotFoundException("Account Not Found");
        
        _database.Delete(currentAcc);
        _database.SaveChanges();
    }

    public IList<ReviewDTO> GetAccountReviews(int id)
    {
        var reviewQuery = _database
            .Get<Review>()
            .Where(new ReviewsByAccountIdSpec(id));

        return _mapper.ProjectTo<ReviewDTO>(reviewQuery).ToList();

    }

    public IList<TripDTO> GetAccountTrips(int id)
    {
        var journeyQuery = _database
            .Get<AccountTrip>()
            .Where(new AccountJourneysByAccountIdSpec(id))
            .Select(x => x.Trip);

        return _mapper
            .ProjectTo<TripDTO>(journeyQuery)
            .ToList();
    }
}
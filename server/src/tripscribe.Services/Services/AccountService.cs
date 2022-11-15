using AutoMapper;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.Accounts;
using tripscribe.Services.DTOs;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Services.Services;

public class AccountService : IAccountService
{
    private readonly ITripscribeDatabase _database; 
    private readonly IMapper _mapper;
    public AccountService(ITripscribeDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public IList<AccountDTO> GetAccount(int id)
    {
        var accountQuery = _database
            .Get<Account>()
            .Where(new AccountByIdSpec(id));

        return _mapper
            .ProjectTo<AccountDTO>(accountQuery)
            .ToList();
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

    public void UpdateAccount(int id, AccountDTO account)
    {

        var currentAcc = _database
            .Get<Account>()
            .FirstOrDefault(new AccountByIdSpec(id));

        if (currentAcc == null) throw new Exception("Not Found");

        _mapper.Map(account, currentAcc);

        _database.SaveChanges();
    }

}
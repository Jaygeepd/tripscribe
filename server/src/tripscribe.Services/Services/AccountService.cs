using AutoMapper;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public class AccountService : IAccountService
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    public AccountService(ITripscribeDatabase database, IMapper _mapper) => 
        _database = database;

    public IList<AccountDTO> GetAccounts()
    {
        var accountDTOs = _mapper.ProjectTo<AccountDTO>(
            _database.Get<Account>()
        ).ToList();
        
        return accountDTOs;
    }

}
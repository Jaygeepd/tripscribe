using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.Accounts;
using tripscribe.Services.DTOs;
using BC = BCrypt.Net.BCrypt;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Services.Services;

public class AuthenticationService: IAuthenticateService
{
    private readonly ITripscribeDatabase _database; 
    private readonly IMapper _mapper;
    public AuthenticationService(ITripscribeDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public AccountDTO? Authenticate(string email, string password)
    {
        var account = _database.Get<Account>().Where(new AccountByEmailSpec(email)).SingleOrDefault();
        
        if (account is null || BC.Verify(password, account.Password)) return null;

        return _mapper.Map<AccountDTO>(account);
    }

    
}
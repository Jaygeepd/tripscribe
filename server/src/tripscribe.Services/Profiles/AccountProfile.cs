using AutoMapper;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Services.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        ConfigureDomainToDto();
        ConfigureDtoToDomain();
    }

    private void ConfigureDomainToDto()
    {
        CreateMap<Account, AccountDTO>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<AccountDTO, Account>();

    }
}
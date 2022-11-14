using AutoMapper;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Services.Profiles;

public class AccountServiceProfile : Profile
{
    public AccountServiceProfile()
    {
        ConfigureDomainToDto();
        ConfigureDtoToDomain();
    }

    private void ConfigureDomainToDto()
    {
        CreateMap<Account, AccountDTO>()
            .ForMember(d => d.Journeys, o=> 
                o.MapFrom(x => x.AccountJourneys));
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<AccountDTO, Account>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Password, o => o.Ignore());
    }
}
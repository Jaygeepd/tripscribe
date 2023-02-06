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
            .ForMember(d=>d.Trips, s => s.MapFrom(x => x.AccountTrips.Select(y=>y.Trip)));
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<AccountDTO, Account>();
        CreateMap<AccountDTO, AccountTrip>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.AccountId, o => o.MapFrom(x => x.Id));
    }
}
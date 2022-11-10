using AutoMapper;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;

namespace tripscribe.Services.Profiles;

public class AccountServiceProfile : Profile
{
    public AccountServiceProfile()
    {
        ConfigureDomainToViewModel();
    }

    private void ConfigureDomainToViewModel()
    {
        CreateMap<Account, AccountDTO>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
}
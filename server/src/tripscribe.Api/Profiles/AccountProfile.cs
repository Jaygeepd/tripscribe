using AutoMapper;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Dal.Models;

namespace tripscribe.Api.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        ConfigureDomainToViewModel();
    }

    private void ConfigureDomainToViewModel()
    {
        CreateMap<Account, AccountViewModel>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.TestDate, o => o.MapFrom(x => DateTime.Now));
    }
}
using AutoMapper;
using tripscribe.Api.ViewModels.Stop;
using tripscribe.Dal.Models;

namespace tripscribe.Api.Profiles;

public class StopProfile : Profile
{
    public StopProfile()
    {
        ConfigureDomainToViewModel();
    }

    private void ConfigureDomainToViewModel()
    {
        CreateMap<Stop, StopViewModel>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
}
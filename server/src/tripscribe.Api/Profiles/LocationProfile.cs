using AutoMapper;
using tripscribe.Api.ViewModels.Locations;
using tripscribe.Dal.Models;

namespace tripscribe.Api.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        ConfigureDomainToViewModel();
    }

    private void ConfigureDomainToViewModel()
    {
        CreateMap<Location, LocationViewModel>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
}
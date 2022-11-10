using AutoMapper;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Dal.Models;

namespace tripscribe.Api.Profiles;

public class JourneyProfile : Profile
{
    public JourneyProfile()
    {
        ConfigureDomainToViewModel();
    }

    private void ConfigureDomainToViewModel()
    {
        CreateMap<Journey, JourneyViewModel>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
}
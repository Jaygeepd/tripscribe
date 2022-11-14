using AutoMapper;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Services.DTOs;

namespace tripscribe.Api.Profiles;

public class JourneyProfile : Profile
{
    public JourneyProfile()
    {
        ConfigureDomainToViewModel();
    }

    private void ConfigureDomainToViewModel()
    {
        CreateMap<JourneyDTO, JourneyViewModel>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
}
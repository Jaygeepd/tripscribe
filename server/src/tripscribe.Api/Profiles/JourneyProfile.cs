using AutoMapper;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Services.DTOs;

namespace tripscribe.Api.Profiles;

public class JourneyProfile : Profile
{
    public JourneyProfile()
    {
        ConfigureDtoToViewModel();
        ConfigureViewModelToDto();
    }

    private void ConfigureDtoToViewModel()
    {
        CreateMap<JourneyDTO, JourneyViewModel>()
            .ForMember(d => d.Id, o => o.Ignore());
    }

    private void ConfigureViewModelToDto()
    {
        CreateMap<CreateJourneyViewModel, JourneyDTO>();
        CreateMap<UpdateJourneyViewModel, JourneyDTO>();
    }
}
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
        CreateMap<JourneyDTO, JourneyViewModel>();
    }

    private void ConfigureViewModelToDto()
    {
        CreateMap<CreateJourneyViewModel, JourneyDTO>()
            .ForMember(d => d.Timestamp, 
                o => o.MapFrom(x => DateTime.UtcNow));
        CreateMap<UpdateJourneyViewModel, JourneyDTO>();
    }
}
using AutoMapper;
using tripscribe.Api.ViewModels.Journeys;
using tripscribe.Services.DTOs;

namespace tripscribe.Api.Profiles;

public class TripProfile : Profile
{
    public TripProfile()
    {
        ConfigureDtoToViewModel();
        ConfigureViewModelToDto();
    }

    private void ConfigureDtoToViewModel()
    {
        CreateMap<TripDTO, TripViewModel>();
    }

    private void ConfigureViewModelToDto()
    {
        CreateMap<CreateTripViewModel, TripDTO>()
            .ForMember(d => d.Timestamp, 
                o => o.MapFrom(x => DateTime.UtcNow));
        CreateMap<UpdateTripViewModel, TripDTO>();
    }
}
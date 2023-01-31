using AutoMapper;
using tripscribe.Api.ViewModels.Trips;
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
        CreateMap<TripDTO, TripDetailViewModel>();
    }

    private void ConfigureViewModelToDto()
    {
        CreateMap<CreateTripViewModel, TripDTO>()
            .ForMember(d => d.Timestamp, 
                o => o.MapFrom(x => DateTime.UtcNow));
        CreateMap<UpdateTripViewModel, TripDTO>();
    }
}
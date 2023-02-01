using AutoMapper;
using tripscribe.Api.ViewModels.Locations;
using tripscribe.Services.DTOs;

namespace tripscribe.Api.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        ConfigureDtoToViewModel();
        ConfigureViewModelToDto();
    }

    private void ConfigureDtoToViewModel()
    {
        CreateMap<LocationDTO, LocationViewModel>();
        CreateMap<LocationDTO, LocationDetailViewModel>();
    }

    private void ConfigureViewModelToDto()
    {
        CreateMap<CreateLocationViewModel, LocationDTO>();
        CreateMap<UpdateLocationViewModel, LocationDTO>();
    }
}
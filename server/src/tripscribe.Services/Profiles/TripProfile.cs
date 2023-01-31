using AutoMapper;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;

namespace tripscribe.Services.Profiles;

public class TripProfile : Profile
{
    public TripProfile()
    {
        ConfigureDomainToDto();
        ConfigureDtoToDomain();
    }

    private void ConfigureDomainToDto()
    {
        CreateMap<Trip, TripDTO>().ForMember(d => d.Stops, s => s.MapFrom(x => x.TripStops));
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<TripDTO, Trip>();
    }
}
using AutoMapper;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;

namespace tripscribe.Services.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        ConfigureDomainToDto();
        ConfigureDtoToDomain();
    }

    private void ConfigureDomainToDto()
    {
        CreateMap<Location, LocationDTO>()
            .ForMember(d => d.Latitude, 
                s => s.MapFrom(x => x.GeoLocation.X))
            .ForMember(d => d.Longitude, 
                s => s.MapFrom(x => x.GeoLocation.Y));
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<LocationDTO, Location>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.StopId, o => o.Ignore());
        // .ForMember(d => d.GeoLocation.X, s => s.MapFrom(x => x.Latitude))
        // .ForMember(d => d.GeoLocation.Y, s => s.MapFrom(x => x.Longitude));
    }
}
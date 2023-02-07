using AutoMapper;
using NpgsqlTypes;
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
            .ForMember(d => d.GeoLocation, o => 
                o.MapFrom(x => new NpgsqlPoint(x.Latitude, x.Longitude)));
    }
}
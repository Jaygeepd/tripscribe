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
        CreateMap<Trip, TripDTO>()
            .ForMember(d => d.Stops, s => s.MapFrom(x => x.TripStops))
            .ForMember(d => d.Accounts, o => o.MapFrom(s => s.AccountTrips.Select(y => y.Account)));
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<TripDTO, Trip>()
            .ForMember(d => d.AccountTrips, o => o.MapFrom(x=>x.Accounts));
    }
}
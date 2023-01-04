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
        CreateMap<Trip, TripDTO>();
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<TripDTO, Trip>();
    }
}
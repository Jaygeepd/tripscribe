﻿using AutoMapper;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;

namespace tripscribe.Services.Profiles;

public class LocationServiceProfile : Profile
{
    public LocationServiceProfile()
    {
        ConfigureDomainToDto();
        ConfigureDtoToDomain();
    }

    private void ConfigureDomainToDto()
    {
        CreateMap<Location, LocationDTO>();
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<LocationDTO, Location>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.StopId, o => o.Ignore());
    }
}
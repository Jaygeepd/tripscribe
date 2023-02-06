using AutoMapper;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;

namespace tripscribe.Services.Profiles;

public class StopProfile : Profile
{
    public StopProfile()
    {
        ConfigureDomainToDto();
        ConfigureDtoToDomain();
    }

    private void ConfigureDomainToDto()
    {
        CreateMap<Stop, StopDTO>().ForMember(d => d.Locations, s => s.MapFrom(x => x.StopLocations));
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<StopDTO, Stop>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
}
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
        CreateMap<Stop, StopDTO>();
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<StopDTO, Stop>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.JourneyId, o => o.Ignore());
    }
}
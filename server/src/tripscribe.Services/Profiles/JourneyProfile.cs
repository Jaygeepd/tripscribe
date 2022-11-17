using AutoMapper;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;

namespace tripscribe.Services.Profiles;

public class JourneyProfile : Profile
{
    public JourneyProfile()
    {
        ConfigureDomainToDto();
        ConfigureDtoToDomain();
    }

    private void ConfigureDomainToDto()
    {
        CreateMap<Journey, JourneyDTO>();
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<JourneyDTO, Journey>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
}
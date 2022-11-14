using AutoMapper;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;

namespace tripscribe.Services.Profiles;

public class JourneyServiceProfile : Profile
{
    public JourneyServiceProfile()
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
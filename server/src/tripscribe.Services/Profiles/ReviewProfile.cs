using AutoMapper;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;

namespace tripscribe.Services.Profiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        ConfigureDomainToDto();
        ConfigureDtoToDomain();
    }

    private void ConfigureDomainToDto()
    {
        CreateMap<Review, ReviewDTO>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<ReviewDTO, Review>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
}
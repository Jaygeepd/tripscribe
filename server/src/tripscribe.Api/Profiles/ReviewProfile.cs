using AutoMapper;
using tripscribe.Api.ViewModels.Reviews;
using tripscribe.Services.DTOs;

namespace tripscribe.Api.Profiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        ConfigureDtoToViewModel();
        ConfigureViewModelToDto();
    }

    private void ConfigureDtoToViewModel()
    {
        CreateMap<ReviewDTO, ReviewViewModel>()
            .ForMember(d => d.Id, o => o.Ignore());
    }

    private void ConfigureViewModelToDto()
    {
        CreateMap<CreateReviewViewModel, ReviewDTO>();
        CreateMap<UpdateReviewViewModel, ReviewDTO>();
    }
}
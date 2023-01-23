using AutoMapper;
using tripscribe.Api.ViewModels.Stop;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;

namespace tripscribe.Api.Profiles;

public class StopProfile : Profile
{
    public StopProfile()
    {
        ConfigureDTOToViewModel();
        ConfigureViewModelToDTO();
    }

    private void ConfigureDTOToViewModel()
    {
        CreateMap<StopDTO, StopViewModel>();
    }

    private void ConfigureViewModelToDTO()
    {
        CreateMap<CreateStopViewModel, StopDTO>();
        CreateMap<UpdateStopViewModel, StopDTO>();
    }
}
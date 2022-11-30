using AutoMapper;
using tripscribe.Api.Extensions;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;

namespace tripscribe.Api.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        ConfigureDTOToViewModel();
        ConfigureViewModelToDTO();
    }

    private void ConfigureDTOToViewModel()
    {
        CreateMap<AccountDTO, AccountViewModel>();

    }

    private void ConfigureViewModelToDTO()
    {
        CreateMap<UpdateAccountViewModel, AccountDTO>().IgnoreAllNull();
        CreateMap<CreateAccountViewModel, AccountDTO>();
    }
}
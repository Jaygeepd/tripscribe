using AutoMapper;
using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;

namespace tripscribe.Api.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        ConfigureDTOToViewModel();
    }

    private void ConfigureDTOToViewModel()
    {
        CreateMap<AccountDTO, AccountViewModel>();

    }
}
using Authentication.Domain.Entities;
using Authentication.Service.Dto;
using AutoMapper;

namespace Authentication.Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Account, AccountDto>();
        CreateMap<AccountDto, Account>();
    }
}
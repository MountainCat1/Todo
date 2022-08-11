using AutoMapper;
using Users.Domain.Entities;
using Users.Service.Dto;

namespace Users.Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}
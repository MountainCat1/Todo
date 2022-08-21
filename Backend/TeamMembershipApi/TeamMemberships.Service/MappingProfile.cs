using AutoMapper;
using TeamMemberships.Domain.Entities;
using TeamMemberships.Service.Commands.CreateMembership;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TeamMembership, MembershipDto>();
        CreateMap<MembershipDto, TeamMembership>();

        CreateMap<MembershipCreateDto, TeamMembership>();
    }
}
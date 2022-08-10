using AutoMapper;
using TeamMemberships.Domain.Entities;
using TeamMemberships.Service.Commands.CreateMembership;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TeamMembership, TeamMembershipDto>();
        CreateMap<TeamMembershipDto, TeamMembership>();

        CreateMap<CreateMembershipDto, TeamMembership>();
    }
}
﻿using AutoMapper;
using Teams.Domain.Entities;
using Teams.Service.Dto;

namespace Teams.Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Team
        CreateMap<Team, TeamDto>();
        CreateMap<TeamDto, Team>();

        CreateMap<CreateTeamDto, Team>();
        CreateMap<UpdateTeamDto, Team>();
    }
}
using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Queries.GetTeam;

public class GetTeamQuery : IQuery<TeamDto>
{
    public Guid Guid { get; set; }
}
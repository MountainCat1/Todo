using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Queries.GetTeam;

public class GetTeamQuery : IQuery<TeamDto>
{
    public GetTeamQuery(Guid teamGuid)
    {
        TeamGuid = teamGuid;
    }

    public Guid TeamGuid { get; set; }
}
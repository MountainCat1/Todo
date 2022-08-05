using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Queries.GetTeamMembers;

public class GetTeamMembersQuery : IQuery<ICollection<TeamMemberDto>>
{
    public GetTeamMembersQuery(Guid teamGuid)
    {
        TeamGuid = teamGuid;
    }

    public Guid TeamGuid { get; set; }
}
using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.Queries.GetMembership;

public class GetMembershipQuery : IQuery<TeamMembershipDto>
{
    public GetMembershipQuery(Guid teamGuid, Guid accountGuid)
    {
        TeamGuid = teamGuid;
        AccountGuid = accountGuid;
    }

    public Guid TeamGuid { get; set; }
    public Guid AccountGuid { get; set; }
}
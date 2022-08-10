using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.Queries.GetMembership;

public class GetMembershipQuery : IQuery<TeamMembershipDto>
{
    public Guid TeamGuid { get; set; }
    public Guid UserGuid { get; set; }
}
using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.Queries.GetMemberships;

public class GetMembershipsQuery : IQuery<ICollection<MembershipDto>>
{
}
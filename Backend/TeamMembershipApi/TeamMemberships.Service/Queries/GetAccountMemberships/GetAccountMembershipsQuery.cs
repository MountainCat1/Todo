using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.Queries.GetAccountMemberships;

public class GetAccountMembershipsQuery : IQuery<ICollection<MembershipDto>>
{
    public GetAccountMembershipsQuery(Guid accountGuid)
    {
        AccountGuid = accountGuid;
    }

    public Guid AccountGuid { get; set; }
}
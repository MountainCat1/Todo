using AutoMapper;
using TeamMemberships.Domain.Repositories;
using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.Queries.GetAccountMemberships;

public class GetAccountMembershipsQueryHandler : IQueryHandler<GetAccountMembershipsQuery, ICollection<MembershipDto>>
{
    private readonly ITeamMembershipRepository _membershipRepository;
    private readonly IMapper _mapper;
    
    public GetAccountMembershipsQueryHandler(ITeamMembershipRepository membershipRepository, IMapper mapper)
    {
        _membershipRepository = membershipRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<MembershipDto>> Handle(GetAccountMembershipsQuery query, CancellationToken cancellationToken)
    {
        var entities = await _membershipRepository
            .GetAsync(x => x.AccountGuid == query.AccountGuid);

        var dto = _mapper.Map<ICollection<MembershipDto>>(entities);

        return dto;
    }
}

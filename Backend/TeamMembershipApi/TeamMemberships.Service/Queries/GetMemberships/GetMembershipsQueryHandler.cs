using AutoMapper;
using TeamMemberships.Domain.Repositories;
using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.Queries.GetMemberships;

public class GetMembershipsQueryHandler : IQueryHandler<GetMembershipsQuery, ICollection<MembershipDto>>
{
    private readonly ITeamMembershipRepository _teamMembershipRepository;
    private readonly IMapper _mapper;

    public GetMembershipsQueryHandler(ITeamMembershipRepository teamMembershipRepository, IMapper mapper)
    {
        _teamMembershipRepository = teamMembershipRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<MembershipDto>> Handle(GetMembershipsQuery query, CancellationToken cancellationToken)
    {
        var entities = await _teamMembershipRepository.GetAllAsync();

        var dto = _mapper.Map<ICollection<MembershipDto>>(entities);

        return dto;
    }
}
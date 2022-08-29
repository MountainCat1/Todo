using AutoMapper;
using TeamMemberships.Domain.Repositories;
using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;
using TeamMemberships.Service.Errors;

namespace TeamMemberships.Service.Queries.GetMembership;

public class GetMembershipQueryHandler : IQueryHandler<GetMembershipQuery, MembershipDto>
{
    private readonly ITeamMembershipRepository _teamMembershipRepository;
    private readonly IMapper _mapper;
    
    public GetMembershipQueryHandler(ITeamMembershipRepository teamMembershipRepository, IMapper mapper)
    {
        _teamMembershipRepository = teamMembershipRepository;
        _mapper = mapper;
    }

    public async Task<MembershipDto> Handle(GetMembershipQuery query, CancellationToken cancellationToken)
    {
        var entity = await _teamMembershipRepository.GetAsync(query.TeamGuid, query.AccountGuid);

        if (entity is null)
            throw new NotFoundError();

        var dto = _mapper.Map<MembershipDto>(entity);

        return dto;
    }
}
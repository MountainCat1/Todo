using AutoMapper;
using TeamMemberships.Domain.Repositories;
using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;
using TeamMemberships.Service.Exceptions;

namespace TeamMemberships.Service.Queries.GetMembership;

public class GetMembershipQueryHandler : IQueryHandler<GetMembershipQuery, TeamMembershipDto>
{
    private readonly ITeamMembershipRepository _teamMembershipRepository;
    private readonly IMapper _mapper;
    
    public GetMembershipQueryHandler(ITeamMembershipRepository teamMembershipRepository, IMapper mapper)
    {
        _teamMembershipRepository = teamMembershipRepository;
        _mapper = mapper;
    }

    public async Task<TeamMembershipDto> Handle(GetMembershipQuery query, CancellationToken cancellationToken)
    {
        var entity = await _teamMembershipRepository.GetAsync(query);

        if (entity is null)
            throw new NotFoundException();

        var dto = _mapper.Map<TeamMembershipDto>(entity);

        return dto;
    }
}
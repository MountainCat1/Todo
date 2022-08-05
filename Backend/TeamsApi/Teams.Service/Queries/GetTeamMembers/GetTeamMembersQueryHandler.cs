using System.Runtime.CompilerServices;
using AutoMapper;
using Teams.Domain.Repositories;
using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Queries.GetTeamMembers;

public class GetTeamMembersQueryHandler : IQueryHandler<GetTeamMembersQuery, ICollection<TeamMemberDto>>
{
    private readonly ITeamRepository _teamRepository;
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly IMapper _mapper;

    public GetTeamMembersQueryHandler(ITeamRepository teamRepository, IMapper mapper, ITeamMemberRepository teamMemberRepository)
    {
        _teamRepository = teamRepository;
        _mapper = mapper;
        _teamMemberRepository = teamMemberRepository;
    }

    public async Task<ICollection<TeamMemberDto>> Handle(GetTeamMembersQuery query, CancellationToken cancellationToken)
    {
        var teamEntity = await _teamRepository.GetRequiredAsync(query.TeamGuid);

        var teamMemberEntities = await _teamMemberRepository
            .GetAsync(x => x.TeamGuid == teamEntity.Guid);

        var dto = _mapper.Map<ICollection<TeamMemberDto>>(teamMemberEntities);

        return dto;
    }
}
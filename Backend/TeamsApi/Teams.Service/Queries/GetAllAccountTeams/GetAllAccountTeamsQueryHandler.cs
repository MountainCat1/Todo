using AutoMapper;
using Teams.Domain.Repositories;
using Teams.Infrastructure.HttpClients;
using Teams.Service.Abstractions;
using Teams.Service.Dto;
using Teams.Service.Queries.GetAllTeamTodos;

namespace Teams.Service.Queries.GetAllAccountTeams;

public class GetAllAccountTeamsQueryHandler : IQueryHandler<GetAllAccountTeamsQuery, ICollection<TeamDto>>
{
    private readonly IMembershipClient _membershipClient;
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper _mapper;
    
    public GetAllAccountTeamsQueryHandler(ITeamRepository teamRepository, IMapper mapper, IMembershipClient membershipClient)
    {
        _teamRepository = teamRepository;
        _mapper = mapper;
        _membershipClient = membershipClient;
    }

    public async Task<ICollection<TeamDto>> Handle(GetAllAccountTeamsQuery query, CancellationToken cancellationToken)
    {
        var memberships = await _membershipClient.GetAccountMembershipsAsync(query.AccountGuid);
        
        var entities = await _teamRepository
            .GetAsync(team => memberships
                .Select(x => x.TeamGuid)
                .Contains(team.Guid));

        var dto = _mapper.Map<ICollection<TeamDto>>(entities);

        return dto;
    }
}
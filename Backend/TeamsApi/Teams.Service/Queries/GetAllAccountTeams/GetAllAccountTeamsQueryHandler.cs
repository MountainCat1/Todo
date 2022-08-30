using AutoMapper;
using Teams.Domain.Repositories;
using Teams.Service.Abstractions;
using Teams.Service.Dto;
using Teams.Service.Queries.GetAllTeamTodos;

namespace Teams.Service.Queries.GetAllAccountTeams;

public class GetAllAccountTeamsQueryHandler : IQueryHandler<GetAllAccountTeamsQuery, ICollection<TeamDto>>
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper _mapper;
    
    public GetAllAccountTeamsQueryHandler(ITeamRepository teamRepository, IMapper mapper)
    {
        _teamRepository = teamRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<TeamDto>> Handle(GetAllAccountTeamsQuery query, CancellationToken cancellationToken)
    {
        var entities = await _teamRepository.GetAsync(team => team.Guid == query.AccountGuid);

        var dto = _mapper.Map<ICollection<TeamDto>>(entities);

        return dto;
    }
}
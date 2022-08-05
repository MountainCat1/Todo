using AutoMapper;
using Teams.Domain.Repositories;
using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Queries.GetAllTeams;

public class GetAllTeamsQueryHandler : IQueryHandler<GetAllTeamsQuery, ICollection<TeamDto>>
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper _mapper;
    
    public GetAllTeamsQueryHandler(ITeamRepository teamRepository, IMapper mapper)
    {
        _teamRepository = teamRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<TeamDto>> Handle(GetAllTeamsQuery query, CancellationToken cancellationToken)
    {
        var entities = await _teamRepository.GetAllAsync();

        var dto = _mapper.Map<ICollection<TeamDto>>(entities);

        return dto;
    }
}
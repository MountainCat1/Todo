using AutoMapper;
using Teams.Domain.Repositories;
using Teams.Service.Abstractions;
using Teams.Service.Dto;
using Teams.Service.Exceptions;

namespace Teams.Service.Queries.GetTeam;

public class GetTeamQueryHandler : IQueryHandler<GetTeamQuery, TeamDto>
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper _mapper;
    
    public GetTeamQueryHandler(ITeamRepository teamRepository, IMapper mapper)
    {
        _teamRepository = teamRepository;
        _mapper = mapper;
    }

    public async Task<TeamDto> Handle(GetTeamQuery query, CancellationToken cancellationToken)
    {
        var entity = await _teamRepository.GetAsync(query.TeamGuid);

        if (entity == null)
            throw new NotFoundException();

        var dto = _mapper.Map<TeamDto>(entity);
        
        return dto;
    }
}

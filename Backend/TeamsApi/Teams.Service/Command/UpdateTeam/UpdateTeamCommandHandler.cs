using AutoMapper;
using Teams.Domain.Entities;
using Teams.Domain.Repositories;
using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Command.UpdateTeam;

public class UpdateTeamCommandHandler : ICommandHandler<UpdateTeamCommand, TeamDto>
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper _mapper;


    public UpdateTeamCommandHandler(ITeamRepository teamRepository, IMapper mapper)
    {
        _teamRepository = teamRepository;
        _mapper = mapper;
    }

    public async Task<TeamDto> Handle(UpdateTeamCommand command, CancellationToken cancellationToken)
    {
        var entity = await _teamRepository.GetRequiredAsync(command.Guid);

        var update = _mapper.Map<Team>(command.Dto);

        var updatedEntity = await _teamRepository.UpdateAsync(command.Guid, update);

        return _mapper.Map<TeamDto>(updatedEntity);
    }
}
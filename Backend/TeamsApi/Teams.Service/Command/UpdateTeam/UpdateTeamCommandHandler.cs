using AutoMapper;
using Teams.Domain.Repositories;
using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Command.UpdateTeam;

public class UpdateTeamCommandHandler : ICommandHandler<UpdateTeamCommand, TeamDto>
{
    private readonly IMapper _mapper;
    private readonly ITeamRepository _teamRepository;

    public UpdateTeamCommandHandler(IMapper mapper, ITeamRepository teamRepository)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
    }

    public async Task<TeamDto> Handle(UpdateTeamCommand command, CancellationToken cancellationToken)
    {
        var updatedEntity = await _teamRepository.UpdateAsync(command.UpdateDto, command.Guid);
        
        await _teamRepository.SaveChangesAsync();

        var resultDto = _mapper.Map<TeamDto>(updatedEntity); 
        
        return _mapper.Map<TeamDto>(resultDto);
    }
}
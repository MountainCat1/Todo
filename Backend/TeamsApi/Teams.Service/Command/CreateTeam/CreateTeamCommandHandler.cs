using AutoMapper;
using Teams.Domain.Entities;
using Teams.Domain.Repositories;
using Teams.Infrastructure.Abstractions;
using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Command.CreateTeam;

public class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommand, TeamDto>
{
    private readonly IMapper _mapper;
    private readonly ITeamRepository _teamRepository;
    
    
    public CreateTeamCommandHandler(IMapper mapper, ITeamRepository teamRepository)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
    }

    public async Task<TeamDto> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Team>(request.Dto);

        var createdEntity = await _teamRepository.CreateAsync(entity);
        await _teamRepository.SaveChangesAsync();

        var createdEntityDto = _mapper.Map<TeamDto>(createdEntity);
        
        return createdEntityDto;
    }
}
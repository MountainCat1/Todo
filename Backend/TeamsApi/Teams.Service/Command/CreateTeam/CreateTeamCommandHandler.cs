using AutoMapper;
using BunnyOwO;
using Teams.Domain.Entities;
using Teams.Domain.Repositories;
using Teams.Infrastructure.ExternalMessages;
using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Command.CreateTeam;

public class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommand, TeamDto>
{
    private readonly IMapper _mapper;
    private readonly ITeamRepository _teamRepository;
    private readonly IMessageSender _messageSender;
    
    
    public CreateTeamCommandHandler(IMapper mapper, ITeamRepository teamRepository, IMessageSender messageSender)
    {
        _mapper = mapper;
        _teamRepository = teamRepository;
        _messageSender = messageSender;
    }

    public async Task<TeamDto> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Team>(command.Dto);

        var createdEntity = await _teamRepository.CreateAsync(entity);

        var integrationEvent = new TeamCreatedMessage(createdEntity.Guid, command.AccountGuid);
        
        _messageSender.PublishEvent(integrationEvent, "team.event.created", "team.team-created.exchange");
        
        await _teamRepository.SaveChangesAsync();

        var createdEntityDto = _mapper.Map<TeamDto>(createdEntity);
        
        return createdEntityDto;
    }
}
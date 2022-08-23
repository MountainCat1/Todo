using BunnyOwO;
using MediatR;
using TeamMemberships.Domain.Enums;
using TeamMemberships.Infrastructure.Events.External;
using TeamMemberships.Service.Commands.CreateMembership;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.EventHandlers;

public class TeamCreatedEventHandler : IEventHandler<TeamCreatedEvent>
{
    private readonly IMediator _mediator;

    public TeamCreatedEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<bool> HandleAsync(TeamCreatedEvent @event)
    {
        var dto = new MembershipCreateDto()
        {
            AccountGuid = @event.CreatorAccountGuid,
            TeamGuid = @event.TeamGuid,
            Role = UserRole.Administrator
        };
        var command = new CreateMembershipCommand(dto);

        await _mediator.Send(command);
        
        return true;
    }

    public void ConfigureReceiver(IEventReceiver receiver)
    {
        receiver.QueueName = "membership.team-created.queue";
    }
}
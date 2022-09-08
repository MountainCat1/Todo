using BunnyOwO;
using MediatR;
using TeamMemberships.Domain.Enums;
using TeamMemberships.Infrastructure.Messages.External;
using TeamMemberships.Service.Commands.CreateMembership;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.MessageHandlers;

public class TeamCreatedEventHandler : IMessageHandler<TeamCreatedMessage>
{
    private readonly IMediator _mediator;

    public TeamCreatedEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<bool> HandleAsync(TeamCreatedMessage message)
    {
        var dto = new MembershipCreateDto()
        {
            AccountGuid = message.CreatorAccountGuid,
            TeamGuid = message.TeamGuid,
            Role = UserRole.Administrator
        };
        var command = new CreateMembershipCommand(dto);

        await _mediator.Send(command);
        
        return true;
    }

    public void ConfigureReceiver(IMessageReceiver receiver)
    {
        receiver.QueueName = "membership.team-created.queue";
    }
}
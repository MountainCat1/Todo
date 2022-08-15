using BunnyOwO;
using MediatR;
using Users.Infrastructure.Events;
using Users.Service.Commands.CreateUser;
using Users.Service.Dto;

namespace Users.Service.EventHandlers;

public class AccountCreatedEventHandler : IEventHandler<AccountCreatedEvent>
{
    private IMediator _mediator;

    public AccountCreatedEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<bool> HandleAsync(AccountCreatedEvent @event)
    {
        var command = new CreateUserCommand(@event.AccountGuid, @event.Username);

        await _mediator.Send(command);
        
        return true;
    }

    public void ConfigureReceiver(IReceiver receiver)
    {
        receiver.QueueName = "user.account-created.queue";
    }
}
using Users.Infrastructure.RabbitMQ;
using Users.Infrastructure.RabbitMQ.Events;

namespace Users.Service.EventHandlers;

public class AccountCreatedEventHandler : IEventHandler<AccountCreatedEvent>
{
    public async Task<bool> HandleAsync(AccountCreatedEvent @event)
    {
        return true;
    }

    public void ConfigureReceiver(IReceiver receiver)
    {
        receiver.QueueName = "account-created-dasdsad";
    }
}
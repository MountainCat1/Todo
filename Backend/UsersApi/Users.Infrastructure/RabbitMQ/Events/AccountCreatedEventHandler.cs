namespace Users.Infrastructure.RabbitMQ.Events;

public class AccountCreatedEventHandler : IEventHandler<AccountCreatedEvent>
{
    public async Task<bool> HandleAsync(AccountCreatedEvent @event)
    {
        return true;
    }
}
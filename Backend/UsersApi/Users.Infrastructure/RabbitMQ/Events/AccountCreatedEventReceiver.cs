using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Users.Infrastructure.Configuration;

namespace Users.Infrastructure.RabbitMQ.Events;

public class AccountCreatedEventReceiver : Receiver<AccountCreatedEvent>
{
    public AccountCreatedEventReceiver(
        IOptions<RabbitMQConfiguration> rabbitMqConfiguration, 
        ILogger<Receiver<AccountCreatedEvent>> parentLogger,
        ILogger<AccountCreatedEventReceiver> logger) : base(rabbitMqConfiguration, parentLogger)
    {
    }
}
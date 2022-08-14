using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Users.Infrastructure.Abstractions;
using Users.Infrastructure.Configuration;

namespace Users.Infrastructure.Services.Receivers;

public class AccountCreatedEventReceiver : RabbitMQReceiver
{
    private readonly ILogger<AccountCreatedEventReceiver> _logger;

    public AccountCreatedEventReceiver(
        IOptions<RabbitMQConfiguration> rabbitMqConfiguration, 
        ILogger<RabbitMQReceiver> parentLogger, 
        ILogger<AccountCreatedEventReceiver> logger) 
        : base(rabbitMqConfiguration, parentLogger)
    {
        _logger = logger;
    }

    public override bool Process(string message)
    {
        _logger.LogInformation($"Processing message: {message}");
        return true;
    }
}
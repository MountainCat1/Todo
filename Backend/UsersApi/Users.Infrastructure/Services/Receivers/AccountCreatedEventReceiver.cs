using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Users.Infrastructure.Abstractions;
using Users.Infrastructure.Configuration;

namespace Users.Infrastructure.Services.Receivers;

public class AccountCreatedEventReceiver : RabbitMQReceiver
{
    public AccountCreatedEventReceiver(
        IOptions<RabbitMQConfiguration> rabbitMqConfiguration, 
        ILogger<RabbitMQReceiver> logger) 
        : base(rabbitMqConfiguration, logger)
    {
        Exchange  = "account-event-created-exchange";
        QueueName = "account-event-created-queue";
    }

    protected override string Exchange { get; }
    protected override string QueueName { get; }

    public override bool Process(string message)
    {
        Logger.LogInformation($"Processing message: {message}");
        return true;
    }
}
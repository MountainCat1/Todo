using System.Text;
using System.Text.Json;
using Authentication.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Authentication.Infrastructure.Services;

public interface IRabbitMQClient
{
    public void PublishMessage(string queue, string routingKey, object message);
}

public class RabbitMQClient : IRabbitMQClient
{
    private readonly ILogger<RabbitMQClient> _logger;
    private readonly RabbitMQConfiguration _rabbitMqConfiguration;
    
    private readonly IModel _channel;

    public RabbitMQClient(RabbitMQConfiguration rabbitMqConfiguration, ILogger<RabbitMQClient> logger)
    {
        _rabbitMqConfiguration = rabbitMqConfiguration;
        _logger = logger;

        var factory = new ConnectionFactory()
        {
            HostName = rabbitMqConfiguration.HostName,
            Password = rabbitMqConfiguration.Password,
            UserName = rabbitMqConfiguration.UserName,
            VirtualHost = rabbitMqConfiguration.VirtualHost
        };
        
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
    }

    public virtual void PublishMessage(string queue, string routingKey, object message)
    {
        _logger.LogInformation($"Publishing message...\nqueue: {queue}, routingKey: {routingKey} ");

        _channel.ExchangeDeclare(
            "account",
            ExchangeType.Topic,
            false,
            false);

        string msgJson = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(msgJson);

        _channel.BasicPublish(
            exchange: "account",
            routingKey: routingKey,
            basicProperties: null,
            body: body);
    }
}
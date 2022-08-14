using System.Text;
using System.Text.Json;
using Authentication.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Authentication.Infrastructure.Services;

public interface IRabbitMQClient
{
    public void PublishMessage(string routingKey, object message);
}

public class RabbitMQClient : IRabbitMQClient
{
    private readonly ILogger<RabbitMQClient> _logger;
    private readonly RabbitMQConfiguration _rabbitMqConfiguration;
    
    private readonly IModel _channel;

    public RabbitMQClient(IOptions<RabbitMQConfiguration> rabbitMqConfiguration, ILogger<RabbitMQClient> logger)
    {
        _rabbitMqConfiguration = rabbitMqConfiguration.Value;
        _logger = logger;

        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMqConfiguration.HostName,
            Password = _rabbitMqConfiguration.Password,
            UserName = _rabbitMqConfiguration.Username,
            VirtualHost = _rabbitMqConfiguration.VirtualHost
        };
        
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
    }

    public virtual void PublishMessage(string routingKey, object message)
    {
        _logger.LogInformation($"Publishing message... {routingKey}");

        var exchange = routingKey.Replace('.', '-') + "-exchange";
        
        _channel.ExchangeDeclare(
            exchange,
            ExchangeType.Fanout,
            false,
            false);

        string msgJson = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(msgJson);

        _channel.BasicPublish(
            exchange: exchange,
            routingKey: routingKey,
            basicProperties: null,
            body: body);
    }
}
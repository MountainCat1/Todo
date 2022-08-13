using System.Text;
using System.Text.Json;
using Authentication.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Authentication.Infrastructure.Services;


public interface IRabbitMQClient
{
    void PublishMessage(string routingKey, object message);
}

public class RabbitMQClient : IRabbitMQClient
{
    private readonly RabbitMQConfiguration _rabbitMqConfiguration;
    private readonly ILogger<RabbitMQClient> _logger;
    
    private readonly IModel _channel;

    public RabbitMQClient(RabbitMQConfiguration rabbitMqConfiguration, ILogger<RabbitMQClient> logger)
    {
        _rabbitMqConfiguration = rabbitMqConfiguration;
        _logger = logger;
        
        try
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqConfiguration.HostName,
                Password = _rabbitMqConfiguration.Password,
                UserName = _rabbitMqConfiguration.UserName,
                VirtualHost = _rabbitMqConfiguration.VirtualHost
            };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
        }
        catch (Exception ex)
        {
            logger.LogError(-1, ex, "RabbitMQClient init fail");
        }
        _logger = logger;
    }

    public virtual void PublishMessage(string routingKey, object message)
    {
        _logger.LogInformation($"PushMessage,routingKey:{routingKey}");
        _channel.QueueDeclare(queue: "message",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        string msgJson = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(msgJson);
        
        _channel.BasicPublish(exchange: "message",
            routingKey: routingKey,
            basicProperties: null,
            body: body);
    }
}
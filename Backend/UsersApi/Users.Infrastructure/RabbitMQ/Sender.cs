using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Users.Infrastructure.Configuration;

namespace Users.Infrastructure.RabbitMQ;
public interface ISender
{
    void PublishMessage(string routingKey, object message);
}

public class Sender : ISender
{
    private readonly RabbitMQConfiguration _rabbitMqConfiguration;
    private readonly ILogger<Sender> _logger;
    
    private readonly IModel _channel;

    public Sender(IOptions<RabbitMQConfiguration> rabbitMqConfiguration, ILogger<Sender> logger)
    {
        _rabbitMqConfiguration = rabbitMqConfiguration.Value;
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
        _logger.LogInformation($"Publishing RabbitMQ message with routing key: {routingKey}...");

        string msgJson = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(msgJson);
        
        _channel.BasicPublish(exchange: "message",
            routingKey: routingKey,
            basicProperties: null,
            body: body);
    }
}

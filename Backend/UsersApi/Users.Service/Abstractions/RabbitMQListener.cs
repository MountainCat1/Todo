using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Users.Infrastructure.Configuration;

namespace Users.Service.Abstractions;

public abstract class RabbitMQListener : IHostedService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<RabbitMQListener> _logger;

    public RabbitMQListener(RabbitMQConfiguration rabbitMqConfiguration, ILogger<RabbitMQListener> logger)
    {
        _logger = logger;

        try
        {
            var factory = new ConnectionFactory()
            {
                // This is my configuration. Just change it to my own use
                HostName = rabbitMqConfiguration.HostName,
                UserName = rabbitMqConfiguration.UserName,
                Password = rabbitMqConfiguration.Password,
                Port = rabbitMqConfiguration.Port
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        catch (Exception ex)
        {
            _logger.LogError($"RabbitListener init error,ex:{ex.Message}");
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Register();
        return Task.CompletedTask;
    }


    protected string RouteKey;
    protected string QueueName;

    // How to process messages
    public virtual bool Process(string message)
    {
        throw new NotImplementedException();
    }

    // Registered consumer monitoring here
    public void Register()
    {
        _logger.LogInformation($"RabbitListener register,routeKey:{RouteKey}");
        _channel.ExchangeDeclare(exchange: "message", type: "topic");
        _channel.QueueDeclare(queue: QueueName, exclusive: false);
        _channel.QueueBind(queue: QueueName,
            exchange: "message",
            routingKey: RouteKey);
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            var result = Process(message);
            if (result)
            {
                _channel.BasicAck(ea.DeliveryTag, false);
            }
        };
        _channel.BasicConsume(queue: QueueName, consumer: consumer);
    }

    public void DeRegister()
    {
        _connection.Close();
    }


    public Task StopAsync(CancellationToken cancellationToken)
    {
        _connection.Close();
        return Task.CompletedTask;
    }
}
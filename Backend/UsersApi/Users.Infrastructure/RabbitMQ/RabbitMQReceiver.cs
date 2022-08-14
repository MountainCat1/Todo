using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Users.Infrastructure.Configuration;

namespace Users.Infrastructure.RabbitMQ;


public interface IRabbitMQReceiver : IHostedService
{
    string QueueName { get; set; }
    bool Process(string message);
    void Register();
    void DeRegister();
}

public abstract class RabbitMQReceiver :  IRabbitMQReceiver
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<RabbitMQReceiver> _parentLogger;

    public RabbitMQReceiver(IOptions<RabbitMQConfiguration> rabbitMqConfiguration, ILogger<RabbitMQReceiver> parentLogger)
    {
        _parentLogger = parentLogger;

        var factory = new ConnectionFactory()
        {
            HostName = rabbitMqConfiguration.Value.HostName,
            UserName = rabbitMqConfiguration.Value.UserName,
            Password = rabbitMqConfiguration.Value.Password,
            Port = rabbitMqConfiguration.Value.Port,
            VirtualHost = rabbitMqConfiguration.Value.VirtualHost
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Register();
        return Task.CompletedTask;
    }

    public string Exchange { get; set; }
    public string QueueName{ get; set; }
    
    // How to process messages
    public virtual bool Process(string message)
    {
        throw new NotImplementedException();
    }
    
    public void Register()
    {
        _parentLogger.LogInformation($"RabbitListener register");

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
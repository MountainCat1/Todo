using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Users.Infrastructure.Configuration;

namespace Users.Infrastructure.RabbitMQ;


public interface IReceiver : IHostedService
{
    string QueueName { get; set; }
    Task<bool> ProcessAsync(string message);
    void Register();
    void DeRegister();
}
public interface IReceiver<TEvent> : IReceiver
    where TEvent : IEvent
{
}

public class Receiver<TEvent> : IReceiver<TEvent> 
    where TEvent : class, IEvent
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<Receiver<TEvent> > _parentLogger;

    private IEventHandler<TEvent> _eventHandler;

    public Receiver(
        IOptions<RabbitMQConfiguration> rabbitMqConfiguration, 
        ILogger<Receiver<TEvent>> parentLogger)
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
    public string QueueName{ get; set; }
    
    // How to process messages
    public virtual async Task<bool> ProcessAsync(string message)
    {
        TEvent? @event = JsonSerializer.Deserialize<TEvent>(message);

        if (@event == null)
            throw new SerializationException($"Cannot deserialize broker message to {typeof(TEvent).Name}");

        return await _eventHandler.HandleAsync(@event);
    }
    
    public void Register()
    {
        _parentLogger.LogInformation($"Registering {GetType().Name}...");

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            var result = await ProcessAsync(message);
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
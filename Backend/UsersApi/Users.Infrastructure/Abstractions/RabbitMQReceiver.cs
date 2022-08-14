using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Users.Infrastructure.Configuration;

namespace Users.Infrastructure.Abstractions;

public abstract class RabbitMQReceiver : IHostedService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    protected readonly ILogger<RabbitMQReceiver> Logger;

    public RabbitMQReceiver(IOptions<RabbitMQConfiguration> rabbitMqConfiguration, ILogger<RabbitMQReceiver> logger)
    {
        Logger = logger;

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

    protected abstract string Exchange { get; }
    protected abstract string QueueName{ get; }
    
    // How to process messages
    public virtual bool Process(string message)
    {
        throw new NotImplementedException();
    }
    
    public void Register()
    {
        Logger.LogInformation($"RabbitListener register");
        
        _channel.QueueDeclare(queue: QueueName, exclusive: false, durable: true, autoDelete: false);
        
        _channel.QueueBind(
            queue: QueueName,
            exchange: Exchange,
            routingKey: "");
        
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

public static class RabbitMQReceiverExtensions
{
    public static IServiceCollection AddRabbitMqReceiver<T>(this IServiceCollection services)
        where T : RabbitMQReceiver
    {
        services.AddHostedService<T>();
        
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace Users.Infrastructure.RabbitMQ;

public static class RabbitMQReceiverExtensions
{
    public static IServiceCollection AddRabbitMqReceiver<T>(this IServiceCollection services, 
        Action<T> configure)
        where T : class, IRabbitMQReceiver
    {
        services.AddHostedService<T>(provider =>
        {
            var constructors = typeof(T)
                .GetConstructors();
            
            var constructorInfo = constructors.First();

            var constructorParameters = constructorInfo.GetParameters()
                .Select(parameterInfo => provider.GetRequiredService(parameterInfo.ParameterType))
                .ToArray();

            var receiver = (T)constructorInfo.Invoke(constructorParameters);
            
            configure(receiver);

            return receiver;
        });
        
        return services;
    }
}
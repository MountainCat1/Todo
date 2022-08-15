using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Users.Infrastructure.RabbitMQ.Extensions;

public static class RabbitMQReceiverExtensions
{
    public static IServiceCollection AddRabbitMqReceiver<T>(this IServiceCollection services,
        Action<T> configure)
        where T : class, IReceiver
    {
        services.AddHostedService(provider =>
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

    /// <summary>
    /// Registers event receivers based on registered event handlers
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public static IServiceCollection AddEventReceivers(this IServiceCollection services, Assembly assembly)
    {
        var provider = services.BuildServiceProvider();
        
        var eventHandlerTypes = assembly
            .GetTypes()
            .Where(type => type.IsClass && type.IsAssignableTo(typeof(IEventHandler)));

        foreach (var eventHandlerType in eventHandlerTypes)
        {
            var eventType = eventHandlerType.GetInterfaces()
                .FirstOrDefault(type => type.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                ?.GetGenericArguments().First();

            if (eventType is null)
                throw new NullReferenceException($"Could not find event type for {eventHandlerType.Name}");

            var receiverType = typeof(Receiver<>).MakeGenericType(eventType);

            Action<IReceiver> configureAction;
            using (var scope = provider.CreateScope())
            {
                var eventHandlerInterfaceType = eventHandlerType.GetInterfaces()
                    .FirstOrDefault(type => type.GetGenericTypeDefinition() == typeof(IEventHandler<>));
                
                configureAction = ((IEventHandler)scope.ServiceProvider.GetRequiredService(eventHandlerInterfaceType!)!)
                    .ConfigureReceiver;
            }

            MethodInfo method = typeof(RabbitMQReceiverExtensions)
                .GetMethod(nameof(AddReceiverHostedService), BindingFlags.Static | BindingFlags.Public)!;

            InvokeGenericMethod(method, receiverType, services, configureAction);
        }

        return services;
    }

    private static object? InvokeGenericMethod(MethodInfo method, Type genericTypeParameter, params object[] arguments)
    {
        // Build a method with the specific type argument you're interested in
        method = method.MakeGenericMethod(genericTypeParameter);
        // The "null" is because it's a static method
        return method.Invoke(null, arguments);
    }
    
    public static void AddReceiverHostedService<T>(IServiceCollection services, Action<T>? configure = null)
        where T : class, IReceiver
    {
        services.AddHostedService(provider =>
        {
            var constructors = typeof(T)
                .GetConstructors();

            var constructorInfo = constructors.First();

            var constructorParameters = constructorInfo.GetParameters()
                .Select(parameterInfo => provider.GetRequiredService(parameterInfo.ParameterType))
                .ToArray();

            var receiver = (T)constructorInfo.Invoke(constructorParameters);

            if (configure is not null)
                configure(receiver);

            return receiver;
        });
    }
}
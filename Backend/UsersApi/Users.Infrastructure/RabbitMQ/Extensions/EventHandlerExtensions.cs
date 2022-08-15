using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure.RabbitMQ.Events;

namespace Users.Infrastructure.RabbitMQ.Extensions;

public static class EventHandlerExtensions
{
    /// <summary>
    /// Registers all event handlers found in specified assembly
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public static IServiceCollection AddEventHandlers(this IServiceCollection services, Assembly assembly)
    {
        var eventHandlerTypes = assembly
            .GetTypes()
            .Where(type => type.IsClass && type.IsAssignableTo(typeof(IEventHandler)));

        foreach (var eventHandlerType in eventHandlerTypes)
        {
            var eventHandlerInterfaceType = eventHandlerType.GetInterfaces()
                .FirstOrDefault(type => type.GetGenericTypeDefinition() == typeof(IEventHandler<>));
            
            if(eventHandlerInterfaceType is null)
                throw new NullReferenceException($"Event handlers need to inherit {typeof(IEventHandler<>).Name}");
            
            services.AddScoped(eventHandlerInterfaceType, eventHandlerType);
        }
        
        return services;
    }
    
}
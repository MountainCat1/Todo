using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Users.Infrastructure.RabbitMQ.Extensions;

public static class EventBusExtensions
{
    public static IServiceCollection AddEventBus(this IServiceCollection serviceCollection,
        Assembly eventHandlersAssemblyMarker,
        Assembly eventReceiversAssemblyMarker)
    {
        serviceCollection.AddSender();
        serviceCollection.AddEventHandlers(eventHandlersAssemblyMarker);
        serviceCollection.AddEventReceivers(eventReceiversAssemblyMarker);

        return serviceCollection;
    }
    
    public static IServiceCollection AddEventBus(this IServiceCollection serviceCollection,
        Type eventHandlersAssemblyMarker,
        Type eventReceiversAssemblyMarker)
    {
        serviceCollection.AddSender();
        serviceCollection.AddEventHandlers(eventHandlersAssemblyMarker.Assembly);
        serviceCollection.AddEventReceivers(eventReceiversAssemblyMarker.Assembly);

        return serviceCollection;
    }
    
    public static IServiceCollection AddEventBus(this IServiceCollection serviceCollection,
        IEnumerable<Type> eventHandlersAssemblyMarkers,
        IEnumerable<Type> eventReceiversAssemblyMarkers)
    {
        serviceCollection.AddSender();
        serviceCollection.AddEventHandlers(eventHandlersAssemblyMarkers.Select(type => type.Assembly).ToArray());
        serviceCollection.AddEventReceivers(eventReceiversAssemblyMarkers.Select(type => type.Assembly).ToArray());

        return serviceCollection;
    }
}
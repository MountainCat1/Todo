using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace Teams.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthorizationHandlers(
        this IServiceCollection serviceCollection,
        Assembly assemblyMarker)
    {
        var types = assemblyMarker.GetTypes()
            .Where(type => type
                .GetInterfaces()
                .Any(interfaceType => interfaceType == typeof(IAuthorizationHandler)));

        foreach (var type in types)
        {
            serviceCollection.AddSingleton(typeof(IAuthorizationHandler), type);
        }

        return serviceCollection;
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace Users.Infrastructure.RabbitMQ.Extensions;

public static class SenderExtensions
{
    public static IServiceCollection AddSender(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ISender, Sender>();
        return serviceCollection;
    }
}
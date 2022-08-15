namespace Users.Infrastructure.RabbitMQ;

public interface IEventHandler
{
}
public interface IEventHandler<in TEvent> : IEventHandler
    where TEvent : class, IEvent
{
    public Task<bool> HandleAsync(TEvent @event);
}

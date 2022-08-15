namespace Users.Infrastructure.RabbitMQ.Events;

public class AccountCreatedEvent : IEvent
{
    public Guid AccountGuid { get; set; }
}
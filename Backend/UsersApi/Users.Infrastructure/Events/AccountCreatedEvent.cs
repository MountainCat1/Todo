using BunnyOwO;

namespace Users.Infrastructure.Events;

public class AccountCreatedEvent : IEvent
{
    public Guid AccountGuid { get; set; }
    public string Username { get; set; }
}
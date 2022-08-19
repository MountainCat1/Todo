using BunnyOwO;

namespace Users.Infrastructure.Events.External;

public class AccountCreatedEvent : IEvent
{
    public Guid AccountGuid { get; set; }
    public Guid UserGuid { get; set; }
    public string Username { get; set; }
}
using BunnyOwO;

namespace Authentication.Infrastructure.Events;

public class AccountCreatedEvent : IEvent
{
    public AccountCreatedEvent(Guid accountGuid, string username, Guid userGuid)
    {
        AccountGuid = accountGuid;
        Username = username;
        UserGuid = userGuid;
    }

    public Guid AccountGuid { get; set; }
    public Guid UserGuid { get; set; }
    public string Username { get; set; }
}
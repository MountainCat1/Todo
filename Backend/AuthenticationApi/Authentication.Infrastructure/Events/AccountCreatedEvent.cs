using BunnyOwO;

namespace Authentication.Infrastructure.Events;

public class AccountCreatedEvent : IEvent
{
    public AccountCreatedEvent(Guid accountGuid, string username)
    {
        AccountGuid = accountGuid;
        Username = username;
    }

    public Guid AccountGuid { get; set; }
    public string Username { get; set; }
}
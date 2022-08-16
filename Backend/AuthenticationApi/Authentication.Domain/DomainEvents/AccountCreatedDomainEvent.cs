using BunnyOwO;

namespace Authentication.Domain.DomainEvents;

public class AccountCreatedDomainEvent : IEvent
{
    public AccountCreatedDomainEvent(Guid accountGuid, string username)
    {
        AccountGuid = accountGuid;
        Username = username;
    }

    public Guid AccountGuid { get; set; }
    public string Username { get; set; }
}
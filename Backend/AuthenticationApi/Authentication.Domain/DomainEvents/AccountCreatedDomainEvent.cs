using BunnyOwO;

namespace Authentication.Domain.DomainEvents;

public class AccountCreatedDomainEvent : IEvent
{
    public AccountCreatedDomainEvent(Guid accountGuid)
    {
        AccountGuid = accountGuid;
    }

    public Guid AccountGuid { get; set; }
}
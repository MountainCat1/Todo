using BunnyOwO;

namespace Authentication.Infrastructure.Events;

public class AccountCreatedDomainEvent : IEvent
{
    public AccountCreatedDomainEvent(Guid accountGuid)
    {
        AccountGuid = accountGuid;
    }

    public Guid AccountGuid { get; set; }
}
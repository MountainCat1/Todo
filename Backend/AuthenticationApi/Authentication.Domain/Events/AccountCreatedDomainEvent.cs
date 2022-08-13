namespace Authentication.Domain.Events;

public class AccountCreatedDomainEvent
{
    public AccountCreatedDomainEvent(Guid accountGuid)
    {
        AccountGuid = accountGuid;
    }

    public Guid AccountGuid { get; set; }
}
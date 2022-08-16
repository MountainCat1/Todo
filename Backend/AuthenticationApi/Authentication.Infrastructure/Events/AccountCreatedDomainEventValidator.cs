using FluentValidation;

namespace Authentication.Infrastructure.Events;

public class AccountCreatedDomainEventValidator : AbstractValidator<AccountCreatedDomainEvent>
{
    public AccountCreatedDomainEventValidator()
    {
        RuleFor(x => x.AccountGuid).NotEmpty().NotNull();
    }
}
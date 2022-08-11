using FluentValidation;

namespace TeamMemberships.Service.Commands.CreateMembership;

public class CreateMembershipCommandValidator : AbstractValidator<CreateMembershipCommand>
{
    public CreateMembershipCommandValidator()
    {
    }
}
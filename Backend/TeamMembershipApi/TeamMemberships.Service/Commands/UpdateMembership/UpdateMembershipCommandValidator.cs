using FluentValidation;

namespace TeamMemberships.Service.Commands.UpdateMembership;

public class UpdateMembershipCommandValidator : AbstractValidator<UpdateMembershipCommand>
{
    public UpdateMembershipCommandValidator()
    {
    }
}
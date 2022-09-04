using FluentValidation;

namespace Users.Service.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(command => command.UpdateDto).NotNull();
        RuleFor(command => command.UpdateDto).NotEmpty();
        RuleFor(command => command.UpdateDto.Username).NotEmpty();
        RuleFor(command => command.UpdateDto.Username).NotNull();
        RuleFor(command => command.UpdateDto.Username).Length(3, 32);
    }
}
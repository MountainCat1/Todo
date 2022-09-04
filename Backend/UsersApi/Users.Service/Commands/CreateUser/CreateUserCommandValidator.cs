using FluentValidation;

namespace Users.Service.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.CreateDto).NotNull();
        RuleFor(command => command.CreateDto).NotEmpty();
        RuleFor(command => command.CreateDto.Username).NotEmpty();
        RuleFor(command => command.CreateDto.Username).NotNull();
        RuleFor(command => command.CreateDto.Username).Length(3, 32);
    }
}
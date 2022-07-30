using FluentValidation;

namespace Todos.Service.Validators;

public class TodoValidator : AbstractValidator<Domain.Entities.Todo>
{
    public TodoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().Length(1, 32);
        RuleFor(x => x.Description).MaximumLength(256);
    }
}
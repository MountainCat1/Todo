using FluentValidation;

namespace Todos.Service.Commands.UpdateTodo;

public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(x => x.Dto.Title).NotEmpty();
        RuleFor(x => x.Dto.Title).Length(1, 32);
        RuleFor(x => x.Dto.Description).MaximumLength(256);
        RuleFor(x => x.Dto.Tags.Count).LessThan(10);
    }
}
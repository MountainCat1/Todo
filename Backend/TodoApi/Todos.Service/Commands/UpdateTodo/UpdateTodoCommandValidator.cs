using FluentValidation;

namespace Todos.Service.Commands.UpdateTodo;

public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(x => x.UpdateDto.Title).NotEmpty();
        RuleFor(x => x.UpdateDto.Title).Length(1, 32);
        RuleFor(x => x.UpdateDto.Description).MaximumLength(256);
        RuleFor(x => x.UpdateDto.Tags.Count).LessThan(10);
    }
}
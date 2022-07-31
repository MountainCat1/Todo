using FluentValidation;
using Todos.Service.Dto;

namespace Todos.Service.Commands.CreateTodo;

public class CreateTodoValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoValidator()
    {
        RuleFor(x => x.Dto.Title).NotEmpty();
        RuleFor(x => x.Dto.Title).Length(1, 32);
        RuleFor(x => x.Dto.Description).MaximumLength(256);
        RuleFor(x => x.Dto.Tags.Count).LessThan(10);
    }
}
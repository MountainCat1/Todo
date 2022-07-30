using Todos.Service.Dto;

namespace Todos.Service.Commands;

public class CreateTodoCommand : ICommand
{
    public CreateTodoDto CreateTodoDto { get; init; }
}
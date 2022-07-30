using Todo.Service.Dto;

namespace Todo.Service.Commands;

public class CreateTodoCommand : ICommand
{
    public CreateTodoDto CreateTodoDto { get; init; }
}
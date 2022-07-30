using Todos.Service.Dto;

namespace Todos.Service.Commands;

public class UpdateTodoCommand : ICommand
{
    public Guid Guid { get; init; }
    public TodoDto TodoDto { get; init; }
}
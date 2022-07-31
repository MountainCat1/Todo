using MediatR;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Commands.UpdateTodo;

public class UpdateTodoCommand : ICommand
{
    public UpdateTodoCommand(Guid guid, TodoDto dto)
    {
        Guid = guid;
        Dto = dto;
    }

    public Guid Guid { get; init; }
    public TodoDto Dto { get; init; }
}
using MediatR;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Commands.UpdateTodo;

public class UpdateTodoCommand : ICommand
{
    public UpdateTodoCommand(Guid guid, TodoDto todoDto)
    {
        Guid = guid;
        TodoDto = todoDto;
    }

    public Guid Guid { get; init; }
    public TodoDto TodoDto { get; init; }
}
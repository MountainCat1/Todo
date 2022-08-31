using MediatR;
using Todos.Infrastructure.MessageDto;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Commands.UpdateTodo;

public class UpdateTodoCommand : ICommand
{
    public UpdateTodoCommand(Guid guid, UpdateTodoDto updateDto)
    {
        Guid = guid;
        UpdateDto = updateDto;
    }

    public Guid Guid { get; }
    public UpdateTodoDto UpdateDto { get; }
}
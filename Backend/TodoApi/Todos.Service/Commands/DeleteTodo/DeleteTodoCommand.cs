using MediatR;
using Todos.Service.Abstractions;

namespace Todos.Service.Commands.DeleteTodo;

public class DeleteTodoCommand : ICommand
{
    public DeleteTodoCommand(Guid guid)
    {
        Guid = guid;
    }

    public Guid Guid { get; init; }
}
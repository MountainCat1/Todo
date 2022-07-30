using MediatR;

namespace Todos.Service.Commands.DeleteTodo;

public class DeleteTodoCommand : IRequest
{
    public DeleteTodoCommand(Guid guid)
    {
        Guid = guid;
    }

    public Guid Guid { get; init; }
}
using MediatR;
using Todos.Service.Dto;

namespace Todos.Service.Queries.GetTodo;

public class GetTodoQuery : IRequest<TodoDto?>
{
    public Guid Guid { get; init; }
}
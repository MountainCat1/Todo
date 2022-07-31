using MediatR;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Queries.GetTodo;

public class GetTodoQuery : IQuery<TodoDto?>
{
    public Guid Guid { get; init; }
}
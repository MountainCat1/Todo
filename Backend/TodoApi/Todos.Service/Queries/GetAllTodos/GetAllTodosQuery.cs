using MediatR;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Queries.GetAllTodos;

public class GetAllTodosQuery : IQuery<ICollection<TodoDto>>
{
}
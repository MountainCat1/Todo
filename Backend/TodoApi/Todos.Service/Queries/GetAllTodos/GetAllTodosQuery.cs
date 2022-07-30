using MediatR;
using Todos.Service.Dto;

namespace Todos.Service.Queries.GetAllTodos;

public class GetAllTodosQuery : IRequest<ICollection<TodoDto>>
{
}
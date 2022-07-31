using Todos.Service.Abstractions;
using Todos.Service.Dto;
using Todos.Service.Queries.GetAllTodos;

namespace Todos.Service.Queries.GetAllFilteredTodos;

public class GetAllFilteredTodosQuery : IQuery<ICollection<TodoDto>>
{
    public GetAllFilteredTodosQuery(Guid? teamGuid, Guid? userGuid)
    {
        TeamGuid = teamGuid;
        UserGuid = userGuid;
    }

    public Guid? TeamGuid { get; set; }
    public Guid? UserGuid { get; set; }
}
using Microsoft.EntityFrameworkCore.Diagnostics;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Queries.GetAllTeamTodos;

public class GetAllTeamTodosQuery : IQuery<ICollection<TodoDto>>
{
    public GetAllTeamTodosQuery(Guid teamGuid)
    {
        TeamGuid = teamGuid;
    }

    public Guid TeamGuid { get; set; }
}
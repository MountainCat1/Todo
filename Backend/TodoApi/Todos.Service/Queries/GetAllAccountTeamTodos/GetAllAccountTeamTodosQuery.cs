using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Queries.GetAllAccountTeamTodos;

public class GetAllAccountTeamTodosQuery : IQuery<ICollection<TodoDto>>
{
    public GetAllAccountTeamTodosQuery(Guid? teamGuid, Guid? accountGuid)
    {
        TeamGuid = teamGuid;
        AccountGuid = accountGuid;
    }

    public Guid? TeamGuid { get; set; }
    public Guid? AccountGuid { get; set; }
}
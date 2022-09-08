using Teams.Infrastructure.Dto;
using Teams.Service.Abstractions;

namespace Teams.Service.Queries.GetAllTodos;

public class GetAllTodosQuery : IQuery<ICollection<TodoDto>>
{
    public GetAllTodosQuery(Guid teamGuid, Guid accountGuid)
    {
        TeamGuid = teamGuid;
        AccountGuid = accountGuid;
    }

    public Guid TeamGuid { get; set; }
    public Guid AccountGuid { get; set; }
}
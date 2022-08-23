using Teams.Infrastructure.Dto;
using Teams.Service.Abstractions;

namespace Teams.Service.Queries.GetAllTeamTodos;

public class GetAllTeamTodosQuery : IQuery<ICollection<TodoDto>>
{
    public GetAllTeamTodosQuery(Guid teamGuid)
    {
        TeamGuid = teamGuid;
    }

    public Guid TeamGuid { get; set; }
}
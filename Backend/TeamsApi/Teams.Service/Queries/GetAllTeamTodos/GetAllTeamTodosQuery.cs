using Teams.Infrastructure.Dto;
using Teams.Service.Abstractions;

namespace Teams.Service.Queries.GetAllTeamTodos;

public class GetAllTeamTodosQuery : IQuery<ICollection<TodoDto>>
{
    public Guid TeamGuid { get; set; }
}
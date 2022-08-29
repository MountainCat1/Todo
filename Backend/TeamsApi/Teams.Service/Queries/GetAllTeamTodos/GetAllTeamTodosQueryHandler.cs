using Teams.Infrastructure.Dto;
using Teams.Infrastructure.HttpClients;
using Teams.Service.Abstractions;

namespace Teams.Service.Queries.GetAllTeamTodos;

public class GetAllTeamTodosQueryHandler : IQueryHandler<GetAllTeamTodosQuery, ICollection<TodoDto>>
{
    private readonly ITodoClient _todoClient;

    public GetAllTeamTodosQueryHandler(ITodoClient todoClient)
    {
        _todoClient = todoClient;
    }

    public async Task<ICollection<TodoDto>> Handle(GetAllTeamTodosQuery query, CancellationToken cancellationToken)
    {
        var todos = await _todoClient.GetTeamTodos(query.TeamGuid);
        
        return todos;
    }
}
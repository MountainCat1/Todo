using Teams.Infrastructure.Dto;
using Teams.Infrastructure.HttpClients;
using Teams.Service.Abstractions;

namespace Teams.Service.Queries.GetAllTodos;

public class GetAllTodosQueryHandler : IQueryHandler<GetAllTodosQuery, ICollection<TodoDto>>
{
    private readonly ITodoClient _todoClient;
    
    public GetAllTodosQueryHandler(ITodoClient todoClient)
    {
        _todoClient = todoClient;
    }

    public async Task<ICollection<TodoDto>> Handle(GetAllTodosQuery query, CancellationToken cancellationToken)
    {
        var dto = await _todoClient.GetUserTodosAsync(query.TeamGuid, query.AccountGuid);

        return dto;
    }
}
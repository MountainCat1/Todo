using AutoMapper;
using Todos.Domain.Repositories;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Queries.GetAllTeamTodos;

public class GetAllTeamTodosQueryHandler : IQueryHandler<GetAllTeamTodosQuery, ICollection<TodoDto>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    
    public GetAllTeamTodosQueryHandler(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<TodoDto>> Handle(GetAllTeamTodosQuery query, CancellationToken cancellationToken)
    {
        var entities = await _todoRepository.GetAsync(todo => todo.TeamGuid == query.TeamGuid);

        var dto = _mapper.Map<ICollection<TodoDto>>(entities);
        
        return dto;
    }
}
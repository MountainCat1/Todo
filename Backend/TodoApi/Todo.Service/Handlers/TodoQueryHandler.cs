using AutoMapper;
using Todo.Infrastructure.Repositories;
using Todo.Service.Dto;
using Todo.Service.Queries;

namespace Todo.Service.Handlers;

public interface ITodoQueryHandler : 
    IQueryHandler<GetTodosQuery, ICollection<TodoDto>>,
    IQueryHandler<GetTodoQuery, TodoDto>,
    IQueryHandler<GetFilteredTodosQuery, ICollection<TodoDto>>
{
}

public class TodoQueryHandler : ITodoQueryHandler
{
    private readonly ITodoRepository _repository;

    private readonly IMapper _mapper;

    public TodoQueryHandler(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async ValueTask<ICollection<TodoDto>> Handle(GetTodosQuery query, CancellationToken ct)
    {
        var entities = await _repository.GetAllAsync();

        var dto = _mapper.Map<ICollection<TodoDto>>(entities);
        
        return dto;
    }

    public async ValueTask<TodoDto> Handle(GetTodoQuery query, CancellationToken ct)
    {
        var entity = await _repository.GetAsync(query.Guid);

        var dto = _mapper.Map<TodoDto>(entity);

        return dto;
    }

    public async ValueTask<ICollection<TodoDto>> Handle(GetFilteredTodosQuery query, CancellationToken ct)
    {
        var entities = await _repository.GetAllAsync(query.TeamGuid, query.UserGuid);

        var dto = _mapper.Map<ICollection<TodoDto>>(entities);

        return dto;
    }
}
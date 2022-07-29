using AutoMapper;
using TodoApi.Data.Repositories;
using TodoApi.Domain.Entities;
using TodoApi.Service.Dto;
using TodoApi.Service.Queries;

namespace TodoApi.Service.Handlers;

public interface ITodoQueryHandler : 
    IQueryHandler<GetAllTodosQuery, ICollection<TodoDto>>,
    IQueryHandler<GetTodoQuery, TodoDto>
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

    public async ValueTask<ICollection<TodoDto>> Handle(GetAllTodosQuery query, CancellationToken ct)
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
}
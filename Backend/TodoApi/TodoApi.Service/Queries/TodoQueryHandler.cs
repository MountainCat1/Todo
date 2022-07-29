using AutoMapper;
using TodoApi.Domain.Entities;
using TodoApi.Service.Dto;
using TodoApi.Service.Repositories;

namespace TodoApi.Service.Queries;

public interface ITodoQueryHandler 
    : IQueryHandler<GetAllTodosQuery, ICollection<Todo>>
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
}
using AutoMapper;
using MediatR;
using Todos.Infrastructure.Repositories;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Queries.GetAllTodos;

public class GetAllTodosQueryHandler : IQueryHandler<GetAllTodosQuery, ICollection<TodoDto>>
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;
    
    public GetAllTodosQueryHandler(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ICollection<TodoDto>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();

        var dto = _mapper.Map<ICollection<TodoDto>>(entities);

        return dto;
    }
}
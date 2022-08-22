using AutoMapper;
using MediatR;
using Todos.Domain.Repositories;
using Todos.Infrastructure.Repositories;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Queries.GetAllFilteredTodos;

public class GetAllFilteredTodosQueryHandler : IQueryHandler<GetAllFilteredTodosQuery, ICollection<TodoDto>>
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;

    public GetAllFilteredTodosQueryHandler(IMapper mapper, ITodoRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ICollection<TodoDto>> Handle(GetAllFilteredTodosQuery query, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAsync(x 
            => x.TeamGuid == query.TeamGuid && 
               x.TeamGuid == query.TeamGuid);

        var dto = _mapper.Map<ICollection<TodoDto>>(entities);

        return dto;
    }
}
using AutoMapper;
using MediatR;
using Todos.Infrastructure.Repositories;
using Todos.Service.Dto;

namespace Todos.Service.Queries.GetAllFilteredTodos;

public class GetAllFilteredTodosQueryHandler : IRequestHandler<GetAllFilteredTodosQuery, ICollection<TodoDto>>
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;

    public GetAllFilteredTodosQueryHandler(IMapper mapper, ITodoRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ICollection<TodoDto>> Handle(GetAllFilteredTodosQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.TeamGuid, request.UserGuid);

        var dto = _mapper.Map<ICollection<TodoDto>>(entities);

        return dto;
    }
}
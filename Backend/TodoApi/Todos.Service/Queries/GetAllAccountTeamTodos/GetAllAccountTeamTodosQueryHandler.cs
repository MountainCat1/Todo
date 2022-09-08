using AutoMapper;
using Todos.Domain.Repositories;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Queries.GetAllAccountTeamTodos;

public class GetAllAccountTeamTodosQueryHandler : IQueryHandler<GetAllAccountTeamTodosQuery, ICollection<TodoDto>>
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;

    public GetAllAccountTeamTodosQueryHandler(IMapper mapper, ITodoRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ICollection<TodoDto>> Handle(GetAllAccountTeamTodosQuery query, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAsync(x 
            => x.TeamGuid == query.TeamGuid && 
               x.AccountGuid == query.AccountGuid);

        var dto = _mapper.Map<ICollection<TodoDto>>(entities);

        return dto;
    }
}
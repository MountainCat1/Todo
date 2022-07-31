using AutoMapper;
using MediatR;
using Todos.Infrastructure.Repositories;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Queries.GetTodo;

public class GetTodoQueryHandler : IQueryHandler<GetTodoQuery, TodoDto?>
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;

    public GetTodoQueryHandler(IMapper mapper, ITodoRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<TodoDto?> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAsync(request.Guid);

        var dto = _mapper.Map<TodoDto?>(entity);

        return dto;
    }
}
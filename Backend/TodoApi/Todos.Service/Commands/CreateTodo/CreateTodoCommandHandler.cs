using AutoMapper;
using MediatR;
using Todos.Domain.Entities;
using Todos.Domain.Repositories;
using Todos.Infrastructure.Repositories;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Commands.CreateTodo;

public class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, TodoDto>
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;
    
    public CreateTodoCommandHandler(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TodoDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Todo>(request.Dto);

        var createdEntity = await _repository.CreateAsync(entity);

        var createdEntityDto = _mapper.Map<TodoDto>(createdEntity);
        
        return createdEntityDto;
    }
}
using AutoMapper;
using MediatR;
using Todos.Domain.Entities;
using Todos.Infrastructure.Repositories;

namespace Todos.Service.Commands.CreateTodo;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand>
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;
    
    public CreateTodoCommandHandler(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Todo>(request.Dto);

        await _repository.CreateAsync(entity);
        
        return Unit.Value;
    }
}
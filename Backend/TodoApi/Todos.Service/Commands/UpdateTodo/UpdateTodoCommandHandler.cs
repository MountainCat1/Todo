using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using MediatR;
using Todos.Domain.Entities;
using Todos.Infrastructure.Repositories;

namespace Todos.Service.Commands.UpdateTodo;

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand>
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;
    
    public UpdateTodoCommandHandler(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Todo>(request.TodoDto);

        await _repository.UpdateAsync(request.Guid, entity);
        
        return Unit.Value;
    }
}
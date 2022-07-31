using System.Reflection.Metadata.Ecma335;
using System.Windows.Input;
using AutoMapper;
using MediatR;
using Todos.Domain.Entities;
using Todos.Domain.Repositories;
using Todos.Infrastructure.Repositories;
using Todos.Service.Abstractions;

namespace Todos.Service.Commands.UpdateTodo;

public class UpdateTodoCommandHandler : ICommandHandler<UpdateTodoCommand>
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
        var entity = _mapper.Map<Todo>(request.Dto);

        await _repository.UpdateAsync(request.Guid, entity);
        
        return Unit.Value;
    }
}
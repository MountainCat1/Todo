using AutoMapper;
using MediatR;
using Todos.Infrastructure.Repositories;

namespace Todos.Service.Commands.DeleteTodo;

public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
{    
    private readonly ITodoRepository _repository;

    public DeleteTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Guid);
        
        return Unit.Value;
    }
}
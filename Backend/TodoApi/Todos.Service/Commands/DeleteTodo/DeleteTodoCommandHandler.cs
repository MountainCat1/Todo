using System.Windows.Input;
using AutoMapper;
using MediatR;
using Todos.Domain.Repositories;
using Todos.Infrastructure.Repositories;
using Todos.Service.Abstractions;
using Todos.Service.Exceptions;

namespace Todos.Service.Commands.DeleteTodo;

public class DeleteTodoCommandHandler : ICommand<DeleteTodoCommand>
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
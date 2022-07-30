using MediatR;
using Todos.Infrastructure.Repositories;

namespace Todos.Service.Commands.AddTodoTag;

public class AddTodoTagCommandHandler : IRequestHandler<AddTodoTagCommand>
{
    private readonly ITodoRepository _repository;

    public AddTodoTagCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AddTodoTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAsync(request.TodoGuid);

        entity.Tags.Add(request.Tag);

        await _repository.UpdateAsync(entity.Guid, entity);

        return Unit.Value;
    }
}
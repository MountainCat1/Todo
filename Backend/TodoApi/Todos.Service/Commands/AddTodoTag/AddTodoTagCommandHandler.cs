using AutoMapper;
using MediatR;
using Todos.Infrastructure.Repositories;
using Todos.Service.Abstractions;
using Todos.Service.Dto;

namespace Todos.Service.Commands.AddTodoTag;

public class AddTodoTagCommandHandler : ICommandHandler<AddTodoTagCommand>
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;
    
    public AddTodoTagCommandHandler(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(AddTodoTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAsync(request.TodoGuid);

        entity.Tags.Add(request.Tag);

        var updatedEntity = await _repository.UpdateAsync(entity.Guid, entity);
        
        return Unit.Value;
    }
}
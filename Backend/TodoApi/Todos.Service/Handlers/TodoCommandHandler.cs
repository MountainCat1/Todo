using AutoMapper;
using Todos.Domain.Entities;
using Todos.Service.Queries;
using Todos.Infrastructure.Repositories;
using Todos.Service.Commands;

namespace Todos.Service.Handlers;

public interface ITodoCommandHandler : 
    ICommandHandler<CreateTodoCommand>, 
    ICommandHandler<UpdateTodoCommand>,
    ICommandHandler<DeleteTodoCommand>
{
}

public class TodoCommandHandler : ITodoCommandHandler
{
    private readonly IMapper _mapper;

    private readonly ITodoRepository _todoRepository;

    public TodoCommandHandler(IMapper mapper, ITodoRepository todoRepository)
    {
        _mapper = mapper;
        _todoRepository = todoRepository;
    }
    
    public async ValueTask Handle(CreateTodoCommand command, CancellationToken ct)
    {
        var entity = _mapper.Map<Todo>(command.CreateTodoDto);
        await _todoRepository.CreateAsync(entity);
    }

    public async ValueTask Handle(UpdateTodoCommand command, CancellationToken ct)
    {
        var entity = _mapper.Map<Todo>(command.TodoDto);

        await _todoRepository.UpdateAsync(command.Guid, entity);
    }

    public async ValueTask Handle(DeleteTodoCommand command, CancellationToken ct)
    {
        await _todoRepository.DeleteAsync(command.Guid);
    }
}
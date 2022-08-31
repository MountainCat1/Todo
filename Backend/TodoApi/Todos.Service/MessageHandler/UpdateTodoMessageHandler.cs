using AutoMapper;
using BunnyOwO;
using MediatR;
using Todos.Infrastructure.ExternalMessages;
using Todos.Service.Commands.CreateTodo;
using Todos.Service.Commands.UpdateTodo;
using Todos.Service.Dto;

namespace Todos.Service.MessageHandler;

public class UpdateTodoMessageHandler : IMessageHandler<UpdateTodoMessage>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public UpdateTodoMessageHandler(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<bool> HandleAsync(UpdateTodoMessage message)
    {
        var updateDto = _mapper.Map<UpdateTodoDto>(message.UpdateDto);

        var command = new UpdateTodoCommand(message.TodoGuid, updateDto);

        await _mediator.Send(command);

        return true;
    }
    
    public void ConfigureReceiver(IMessageReceiver messageReceiver)
    {
        messageReceiver.QueueName = "todo.update-todo.queue";
    }
}
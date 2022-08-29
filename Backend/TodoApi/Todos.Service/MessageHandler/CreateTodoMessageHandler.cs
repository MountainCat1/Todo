using AutoMapper;
using BunnyOwO;
using MediatR;
using Todos.Infrastructure.ExternalMessages;
using Todos.Service.Commands.CreateTodo;
using Todos.Service.Dto;

namespace Todos.Service.MessageHandler;

public class CreateTodoMessageHandler : IMessageHandler<CreateTodoMessage>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public CreateTodoMessageHandler(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<bool> HandleAsync(CreateTodoMessage message)
    {
        var createDto = _mapper.Map<CreateTodoDto>(message.CreateDto);

        var command = new CreateTodoCommand(createDto);

        await _mediator.Send(command);

        return true;
    }
    
    public void ConfigureReceiver(IMessageReceiver messageReceiver)
    {
        messageReceiver.QueueName = "todo.create-todo.queue";
    }
}
using AutoMapper;
using BunnyOwO;
using MediatR;
using Todos.Infrastructure.ExternalMessages;
using Todos.Service.Commands.CreateTodo;
using Todos.Service.Dto;

namespace Todos.Service.MessageHandler;

public class CreateTodoMessageHandler : IMessageHandler<CreateTodoMessage>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    
    public CreateTodoMessageHandler(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public async Task<bool> HandleAsync(CreateTodoMessage message)
    {
        var createDto = _mapper.Map<CreateTodoDto>(message.CreateDto);

        var command = new CreateTodoCommand(createDto);

        await _sender.Send(command);

        return true;
    }
    
    public void ConfigureReceiver(IMessageReceiver messageReceiver)
    {
        messageReceiver.QueueName = "todo.create-todo-message.queue";
    }
}
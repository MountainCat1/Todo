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

    public async Task<bool> HandleAsync(CreateTodoMessage @event)
    {
        var createDto = _mapper.Map<CreateTodoDto>()
        
        var command = new CreateTodoCommand()
        
        await _sender.Send()
        
        throw new NotImplementedException();
    }
}
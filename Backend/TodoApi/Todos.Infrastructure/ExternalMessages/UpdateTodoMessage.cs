using BunnyOwO;
using Todos.Infrastructure.MessageDto;

namespace Todos.Infrastructure.ExternalMessages;

public class UpdateTodoMessage : IMessage
{
    public UpdateTodoMessageDto UpdateDto { get; set; }
    public Guid TodoGuid { get; set; }
}
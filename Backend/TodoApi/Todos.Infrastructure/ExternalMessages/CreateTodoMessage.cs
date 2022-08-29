using BunnyOwO;
using Todos.Infrastructure.MessageDto;

namespace Todos.Infrastructure.ExternalMessages;

public class CreateTodoMessage : IMessage
{
    public CreateTodoMessageDto CreateDto { get; set; }
}
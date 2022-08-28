using BunnyOwO;
using Teams.Infrastructure.Dto;

namespace Teams.Service.Messages;

public class CreateTodoMessage : IMessage
{
    public CreateTodoDto CreateTodo { get; set; }
}
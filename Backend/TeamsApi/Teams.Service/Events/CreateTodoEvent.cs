using BunnyOwO;
using Teams.Infrastructure.Dto;

namespace Teams.Service.Events;

public class CreateTodoEvent : IEvent
{
    public CreateTodoDto CreateTodo { get; set; }
}
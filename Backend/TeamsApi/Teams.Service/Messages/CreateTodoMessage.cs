using BunnyOwO;
using Teams.Infrastructure.Dto;

namespace Teams.Service.Messages;

public class CreateTodoMessage : IMessage
{
    public CreateTodoMessage(CreateTodoDto createDto)
    {
        CreateDto = createDto;
    }

    public CreateTodoDto CreateDto { get; set; }
}
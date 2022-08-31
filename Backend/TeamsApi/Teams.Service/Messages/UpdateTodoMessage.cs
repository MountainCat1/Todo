using BunnyOwO;
using Teams.Service.Dto;

namespace Teams.Service.Messages;

public class UpdateTodoMessage : IMessage
{
    public UpdateTodoMessage(Guid todoGuid, UpdateTodoDto updateDto)
    {
        UpdateDto = updateDto;
        TodoGuid = todoGuid;
    }

    public UpdateTodoDto UpdateDto { get; set; }
    public Guid TodoGuid { get; set; }
}
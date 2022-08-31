using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Command.UpdateTodo;

public class UpdateTodoCommand : ICommand
{
    public UpdateTodoCommand(Guid teamGuid, Guid todoGuid, UpdateTodoDto updateTodoDto)
    {
        TeamGuid = teamGuid;
        TodoGuid = todoGuid;
        UpdateTodoDto = updateTodoDto;
    }

    public Guid TeamGuid { get; set; }
    public Guid TodoGuid { get; set; }
    public UpdateTodoDto UpdateTodoDto { get; set; }
}
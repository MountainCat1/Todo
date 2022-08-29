using Teams.Infrastructure.Dto;
using Teams.Service.Abstractions;

namespace Teams.Service.Command.CreateTodo;

public class CreateTodoCommand : ICommand
{
    public CreateTodoCommand(Guid teamGuid, CreateTodoDto createDto)
    {
        TeamGuid = teamGuid;
        CreateDto = createDto;
    }

    public Guid TeamGuid { get; set; }
    public CreateTodoDto CreateDto { get; set; }
}
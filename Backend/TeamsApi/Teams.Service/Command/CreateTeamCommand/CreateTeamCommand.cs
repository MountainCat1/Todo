using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Command.CreateTeamCommand;

public class CreateTeamCommand : ICommand<TeamDto>
{
    public CreateTeamDto Dto { get; set; }
}
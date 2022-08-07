using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Command.CreateTeam;

public class CreateTeamCommand : ICommand<TeamDto>
{
    public CreateTeamCommand(CreateTeamDto dto)
    {
        Dto = dto;
    }

    public CreateTeamDto Dto { get; set; }
}
using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Command.CreateTeam;

public class CreateTeamCommand : ICommand<TeamDto>
{
    public CreateTeamCommand(CreateTeamDto dto, Guid accountGuid)
    {
        Dto = dto;
        AccountGuid = accountGuid;
    }

    public CreateTeamDto Dto { get; }
    public Guid AccountGuid { get; }
}
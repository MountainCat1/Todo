using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Command.UpdateTeam;

public class UpdateTeamCommand : ICommand<TeamDto>
{
    public UpdateTeamCommand(Guid guid, UpdateTeamDto dto)
    {
        Guid = guid;
        Dto = dto;
    }

    public UpdateTeamDto Dto { get; set; }
    public Guid Guid { get; set; }
}
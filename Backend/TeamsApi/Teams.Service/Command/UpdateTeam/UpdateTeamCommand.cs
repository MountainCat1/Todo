using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Command.UpdateTeam;

public class UpdateTeamCommand : ICommand<TeamDto>
{
    public UpdateTeamCommand(Guid teamGuid, UpdateTeamDto updateDto)
    {
        TeamGuid = teamGuid;
        UpdateDto = updateDto;
    }

    public UpdateTeamDto UpdateDto { get; set; }
    public Guid TeamGuid { get; set; }
}
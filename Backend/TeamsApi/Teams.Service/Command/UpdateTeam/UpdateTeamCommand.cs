using Teams.Service.Abstractions;
using Teams.Service.Dto;

namespace Teams.Service.Command.UpdateTeam;

public class UpdateTeamCommand : ICommand<TeamDto>
{
    public UpdateTeamCommand(Guid guid, UpdateTeamDto updateDto)
    {
        Guid = guid;
        UpdateDto = updateDto;
    }

    public UpdateTeamDto UpdateDto { get; set; }
    public Guid Guid { get; set; }
}
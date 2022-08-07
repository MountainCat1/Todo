
using Teams.Service.Abstractions;

namespace Teams.Service.Command.DeleteTeam;

public class DeleteTeamCommand : ICommand
{
    public Guid Guid { get; set; }
}
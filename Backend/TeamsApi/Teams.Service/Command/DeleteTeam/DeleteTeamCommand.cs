
using Teams.Service.Abstractions;

namespace Teams.Service.Command.DeleteTeam;

public class DeleteTeamCommand : ICommand
{
    public DeleteTeamCommand(Guid guid)
    {
        Guid = guid;
    }

    public Guid Guid { get; set; }
}
using TeamMemberships.Service.Abstractions;

namespace TeamMemberships.Service.Commands.DeleteMembership;

public class DeleteMembershipCommand : ICommand
{
    public DeleteMembershipCommand(Guid teamGuid, Guid userGuid)
    {
        TeamGuid = teamGuid;
        UserGuid = userGuid;
    }

    public Guid TeamGuid { get; set; }
    public Guid UserGuid { get; set; }
}
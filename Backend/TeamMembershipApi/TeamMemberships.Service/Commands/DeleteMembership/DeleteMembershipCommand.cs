using TeamMemberships.Service.Abstractions;

namespace TeamMemberships.Service.Commands.DeleteMembership;

public class DeleteMembershipCommand : ICommand
{
    public DeleteMembershipCommand(Guid teamGuid, Guid accountGuid)
    {
        TeamGuid = teamGuid;
        AccountGuid = accountGuid;
    }

    public Guid TeamGuid { get; set; }
    public Guid AccountGuid { get; set; }
}
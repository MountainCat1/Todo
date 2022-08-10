using TeamMemberships.Service.Abstractions;

namespace TeamMemberships.Service.Commands.DeleteMembership;

public class DeleteMembershipCommand : ICommand
{
    public Guid TeamGuid { get; set; }
    public Guid UserGuid { get; set; }
}
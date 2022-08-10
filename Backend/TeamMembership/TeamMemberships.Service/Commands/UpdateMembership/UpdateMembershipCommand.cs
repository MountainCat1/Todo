using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.Commands.UpdateMembership;

public class UpdateMembershipCommand : ICommand<TeamMembershipDto>
{
    public Guid UserGuid { get; set; }
    public Guid TeamGuid { get; set; }

    public MembershipUpdateDto UpdateDto { get; set; }
}
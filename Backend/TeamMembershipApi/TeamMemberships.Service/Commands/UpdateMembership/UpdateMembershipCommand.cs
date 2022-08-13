using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.Commands.UpdateMembership;

public class UpdateMembershipCommand : ICommand<TeamMembershipDto>
{
    public UpdateMembershipCommand(Guid accountGuid, Guid teamGuid, MembershipUpdateDto updateDto)
    {
        AccountGuid = accountGuid;
        TeamGuid = teamGuid;
        UpdateDto = updateDto;
    }

    public Guid AccountGuid { get; set; }
    public Guid TeamGuid { get; set; }

    public MembershipUpdateDto UpdateDto { get; set; }
}
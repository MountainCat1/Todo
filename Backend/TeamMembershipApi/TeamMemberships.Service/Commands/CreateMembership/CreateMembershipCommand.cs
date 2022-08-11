using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.Commands.CreateMembership;

public class CreateMembershipCommand : ICommand<TeamMembershipDto>
{
    public CreateMembershipCommand(MembershipCreateDto membershipCreateDto)
    {
        MembershipCreateDto = membershipCreateDto;
    }

    public MembershipCreateDto MembershipCreateDto { get; set; }
}
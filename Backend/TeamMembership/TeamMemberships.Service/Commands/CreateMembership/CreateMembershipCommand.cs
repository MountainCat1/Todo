using TeamMemberships.Service.Abstractions;
using TeamMemberships.Service.Dto;

namespace TeamMemberships.Service.Commands.CreateMembership;

public class CreateMembershipCommand : ICommand<TeamMembershipDto>
{
    public CreateMembershipCommand(CreateMembershipDto createDto)
    {
        CreateDto = createDto;
    }

    public CreateMembershipDto CreateDto { get; set; }
}
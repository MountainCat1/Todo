using TeamMemberships.Domain.Enums;

namespace TeamMemberships.Service.Dto;

public class MembershipDto
{
    public Guid AccountGuid { get; set; }
    public Guid TeamGuid { get; set; }
    public UserRole Role { get; set; }
}
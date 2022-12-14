using TeamMemberships.Domain.Enums;

namespace TeamMemberships.Service.Dto;

public class MembershipCreateDto
{
    public Guid AccountGuid { get; set; }
    public Guid TeamGuid { get; set; }
    public UserRole? Role { get; set; } = UserRole.Member;
}
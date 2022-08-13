namespace TeamMemberships.Service.Dto;

public class MembershipCreateDto
{
    public Guid AccountGuid { get; set; }
    public Guid TeamGuid { get; set; }
}
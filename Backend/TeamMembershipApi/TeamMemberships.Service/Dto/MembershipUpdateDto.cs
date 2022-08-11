namespace TeamMemberships.Service.Dto;

public class MembershipUpdateDto
{
    public Guid UserGuid { get; set; }
    public Guid TeamGuid { get; set; }
}
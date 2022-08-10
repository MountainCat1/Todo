namespace TeamMemberships.Service.Dto;

public class MembershipCreateDto
{
    public Guid UserGuid { get; set; }
    public Guid TeamGuid { get; set; }
}
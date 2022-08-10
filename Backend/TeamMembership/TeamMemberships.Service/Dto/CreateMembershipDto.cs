namespace TeamMemberships.Service.Dto;

public class CreateMembershipDto
{
    public Guid UserGuid { get; set; }
    public Guid TeamGuid { get; set; }
}
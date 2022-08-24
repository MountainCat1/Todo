namespace Teams.Infrastructure.Dto;

public class MembershipDto
{
    public Guid AccountGuid { get; set; }
    public Guid TeamGuid { get; set; }
    public string Role { get; set; }
}
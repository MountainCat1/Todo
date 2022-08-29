namespace Teams.Infrastructure.Dto;

public class MembershipDto
{
    public Guid AccountGuid { get; set; }
    public Guid TeamGuid { get; set; }
    public int Role { get; set; }
}
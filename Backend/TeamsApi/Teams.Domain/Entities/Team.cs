using System.ComponentModel.DataAnnotations;

namespace Teams.Domain.Entities;

public class Team
{
    [Key]
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public virtual List<TeamMember> Members { get; set; }
    public IEnumerable<Guid> MemberGuids => Members.Select(member => member.UserGuid);
}
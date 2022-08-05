using System.ComponentModel.DataAnnotations;
using Teams.Domain.Abstractions;

namespace Teams.Domain.Entities;

public class Team : IEntity
{
    [Key]
    public Guid Guid { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    
    public virtual List<TeamMember> Members { get; set; }
    public IEnumerable<Guid> MemberGuids => Members.Select(member => member.Guid);
}
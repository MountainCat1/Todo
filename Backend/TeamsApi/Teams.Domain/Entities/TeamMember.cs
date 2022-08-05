using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Teams.Domain.Abstractions;

namespace Teams.Domain.Entities;

public class TeamMember : IEntity
{
    [Key]
    public Guid Guid { get; set; }
    
    [ForeignKey(nameof(Team))]
    public Guid TeamGuid { get; set; }
    public Team Team { get; set; }
}
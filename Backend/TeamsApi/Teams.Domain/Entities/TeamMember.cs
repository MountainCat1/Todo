using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teams.Domain.Entities;

public class TeamMember
{
    [Key]
    public Guid UserGuid { get; set; }
    
    [ForeignKey(nameof(Team))]
    public Guid TeamGuid { get; set; }
    public Team Team { get; set; }
}
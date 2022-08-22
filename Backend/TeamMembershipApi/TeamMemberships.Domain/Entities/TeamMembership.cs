using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamMemberships.Domain.Abstractions;

namespace TeamMemberships.Domain.Entities;

public class TeamMembership : IEntity
{
    public Guid TeamGuid { get; set; }
    public Guid AccountGuid { get; set; }


    [ForeignKey(nameof(Role))]
    public string RoleName { get; set; }
    public Role Role { get; set; }
    
    
    public DateTime JoinTime { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamMemberships.Domain.Abstractions;
using TeamMemberships.Domain.Enums;

namespace TeamMemberships.Domain.Entities;

public class TeamMembership : IEntity
{
    public Guid TeamGuid { get; set; }
    public Guid AccountGuid { get; set; }

    [Required]
    public UserRole Role { get; set; } = UserRole.Member;

    public DateTime JoinTime { get; set; }
}
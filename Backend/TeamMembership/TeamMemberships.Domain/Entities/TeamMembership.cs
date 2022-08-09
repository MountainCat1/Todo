using TeamMemberships.Domain.Abstractions;

namespace TeamMemberships.Domain.Entities;

public class TeamMembership : IEntity
{
    public Guid UserGuid { get; set; }
    public Guid TeamGuid { get; set; }

    public DateTime JoinTime { get; set; }
}
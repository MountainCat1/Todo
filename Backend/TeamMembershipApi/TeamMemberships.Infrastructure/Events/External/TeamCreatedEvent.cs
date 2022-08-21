using BunnyOwO;

namespace TeamMemberships.Infrastructure.Events.External;

public class TeamCreatedEvent : IEvent
{
    public TeamCreatedEvent(Guid teamGuid, Guid creatorAccountGuid)
    {
        TeamGuid = teamGuid;
        CreatorAccountGuid = creatorAccountGuid;
    }

    public Guid TeamGuid { get; }
    public Guid CreatorAccountGuid { get; }
}
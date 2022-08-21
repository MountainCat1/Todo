using BunnyOwO;

namespace Teams.Infrastructure.Events;

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
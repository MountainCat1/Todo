using BunnyOwO;

namespace TeamMemberships.Infrastructure.Messages.External;

public class TeamCreatedMessage : IMessage
{
    public TeamCreatedMessage(Guid teamGuid, Guid creatorAccountGuid)
    {
        TeamGuid = teamGuid;
        CreatorAccountGuid = creatorAccountGuid;
    }

    public Guid TeamGuid { get; }
    public Guid CreatorAccountGuid { get; }
}
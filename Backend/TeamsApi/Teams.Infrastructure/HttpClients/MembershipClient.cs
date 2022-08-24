using Teams.Infrastructure.Dto;

namespace Teams.Infrastructure.HttpClients;

public interface IMembershipClient
{
    public Task<MembershipDto?> GetMembershipAsync(Guid teamGuid, Guid accountGuid);
}

public class MembershipClient : IMembershipClient
{
    // TODO make a client responsible for querying data from membership microservice
    
    public async Task<MembershipDto?> GetMembershipAsync(Guid teamGuid, Guid accountGuid)
    {
        // TODO
        throw new NotImplementedException();
    }
}



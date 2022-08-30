using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Teams.Infrastructure.HttpClients;

namespace Teams.Service.Queries.GetTeam;

public class GetTeamQueryAuthorization : AuthorizationHandler<UseRequestRequirement, GetTeamQuery>
{
    private readonly IMembershipClient _membershipClient;

    public GetTeamQueryAuthorization(IMembershipClient membershipClient)
    {
        _membershipClient = membershipClient;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        UseRequestRequirement requirement,
        GetTeamQuery resource)
    {
        var accountGuid = Guid.Parse(context.User.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value);
        var membership = await _membershipClient.GetMembershipAsync(resource.TeamGuid, accountGuid);
        
        if (membership is not null)
        {
            context.Succeed(requirement);
        }
    }
}
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Teams.Infrastructure.HttpClients;

namespace Teams.Service.Queries.GetAllTeamTodos;

public class GetAllTeamTodosQueryAuthorization : AuthorizationHandler<UseRequestRequirement, GetAllTeamTodosQuery>
{
    private readonly IMembershipClient _membershipClient;

    public GetAllTeamTodosQueryAuthorization(IMembershipClient membershipClient)
    {
        _membershipClient = membershipClient;
    }
    

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        UseRequestRequirement requirement,
        GetAllTeamTodosQuery resource)
    {
        var accountGuid = Guid.Parse(context.User.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value);
        var membership = await _membershipClient.GetMembershipAsync(resource.TeamGuid, accountGuid);
        
        if (membership is not null)
        {
            context.Succeed(requirement);
        }
    }
}
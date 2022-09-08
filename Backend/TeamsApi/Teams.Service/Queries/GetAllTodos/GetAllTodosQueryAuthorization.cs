using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Teams.Infrastructure.HttpClients;

namespace Teams.Service.Queries.GetAllTodos;

public class GetAllTodosQueryAuthorization : AuthorizationHandler<UseRequestRequirement, GetAllTodosQuery>
{
    private IMembershipClient _membershipClient;

    public GetAllTodosQueryAuthorization(IMembershipClient membershipClient)
    {
        _membershipClient = membershipClient;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        UseRequestRequirement requirement,
        GetAllTodosQuery resource)
    {
        var accountGuid = Guid.Parse(context.User.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value);
        var membership = await _membershipClient.GetMembershipAsync(resource.TeamGuid, accountGuid);
        
        if (membership is not null)
        {
            context.Succeed(requirement);
        }
    }
}
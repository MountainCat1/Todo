using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Teams.Domain.Entities;
using Teams.Infrastructure.HttpClients;

namespace Teams.Api.AuthorizationHandlers;

public class TeamAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Team>
{
    private readonly IMembershipClient _membershipClient;

    public TeamAuthorizationHandler(IMembershipClient membershipClient)
    {
        _membershipClient = membershipClient;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        Team resource)
    {
        // TODO, changed ClaimTypes.NameIdentifier to ClaimTypes.Sid
        var accountGuid = Guid.Parse(context.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        var membership = await _membershipClient.GetMembershipAsync(resource.Guid, accountGuid);
        
        if (membership != null) // TODO // && requirement.Name == ResourceOperation.Read)
        {
            context.Succeed(requirement);
        }
    }
}
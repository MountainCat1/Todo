using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Teams.Api.AuthorizationHandlers.Requirements;
using Teams.Domain.Entities;
using Teams.Infrastructure.HttpClients;

namespace Teams.Api.AuthorizationHandlers;

public class TeamAuthorizationHandler : AuthorizationHandler<IsMemberAuthorizationRequirement, Team>
{
    private readonly IMembershipClient _membershipClient;

    public TeamAuthorizationHandler(IMembershipClient membershipClient)
    {
        _membershipClient = membershipClient;
    }
    
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        IsMemberAuthorizationRequirement requirement,
        Team resource)
    {
        var accountGuid = Guid.Parse(context.User.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value);
        var membership = await _membershipClient.GetMembershipAsync(resource.Guid, accountGuid);
        
        if (membership is not null && requirement.Name == Operations.Read.Name)
        {
            context.Succeed(requirement);
        }
    }
}
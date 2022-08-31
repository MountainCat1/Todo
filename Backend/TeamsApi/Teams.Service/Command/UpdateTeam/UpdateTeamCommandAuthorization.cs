using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Teams.Infrastructure.HttpClients;
using Teams.Service.Queries.GetAllTeamTodos;

namespace Teams.Service.Command.UpdateTeam;

public class UpdateTeamCommandAuthorization : AuthorizationHandler<UseRequestRequirement, UpdateTeamCommand>
{
    private readonly IMembershipClient _membershipClient;

    public UpdateTeamCommandAuthorization(IMembershipClient membershipClient)
    {
        _membershipClient = membershipClient;
    }
    

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        UseRequestRequirement requirement,
        UpdateTeamCommand resource)
    {
        var accountGuid = Guid.Parse(context.User.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value);
        var membership = await _membershipClient.GetMembershipAsync(resource.TeamGuid, accountGuid);
        
        if (membership is not null)
        {
            context.Succeed(requirement);
        }
    }
}
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Teams.Infrastructure.HttpClients;
using Teams.Service.Command.CreateTeam;

namespace Teams.Service.Command.CreateTodo;

public class CreateTodoCommandAuthorization : AuthorizationHandler<UseRequestRequirement, CreateTodoCommand>
{
    private readonly IMembershipClient _membershipClient;

    public CreateTodoCommandAuthorization(IMembershipClient membershipClient)
    {
        _membershipClient = membershipClient;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        UseRequestRequirement requirement,
        CreateTodoCommand resource)
    {
        var accountGuid = Guid.Parse(context.User.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value);
        var membership = await _membershipClient.GetMembershipAsync(resource.TeamGuid, accountGuid);
        
        if (membership is not null)
        {
            context.Succeed(requirement);
        }
    }
}
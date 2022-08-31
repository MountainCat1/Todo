using System.Security.Claims;
using BunnyOwO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Teams.Infrastructure.HttpClients;
using Teams.Service.Command.CreateTodo;
using Teams.Service.Messages;

namespace Teams.Service.Command.UpdateTodo;

public class UpdateTodoCommandAuthorization: AuthorizationHandler<UseRequestRequirement, UpdateTodoCommand>
{
    private readonly IMembershipClient _membershipClient;

    public UpdateTodoCommandAuthorization(IMembershipClient membershipClient)
    {
        _membershipClient = membershipClient;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        UseRequestRequirement requirement,
        UpdateTodoCommand resource)
    {
        var accountGuid = Guid.Parse(context.User.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value);
        var membership = await _membershipClient.GetMembershipAsync(resource.TeamGuid, accountGuid);
        
        if (membership is not null)
        {
            context.Succeed(requirement);
        }
    }
}
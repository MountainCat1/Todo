using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Teams.Infrastructure.HttpClients;

namespace Teams.Service.Queries.GetAllTeamTodos;



public class GetAllTeamTodosQueryAuthorization : AuthorizationHandler<OperationAuthorizationRequirement, GetAllTeamTodosQuery>
{
    private readonly IMembershipClient _membershipClient;

    public GetAllTeamTodosQueryAuthorization(IMembershipClient membershipClient)
    {
        _membershipClient = membershipClient;
    }
    

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        OperationAuthorizationRequirement requirement,
        GetAllTeamTodosQuery resource)
    {
        var accountGuid = Guid.Parse(context.User.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value);
        var membership = await _membershipClient.GetMembershipAsync(resource.TeamGuid, accountGuid);
        
        if (membership is not null && requirement.Name == Operations.Read.Name)
        {
            context.Succeed(requirement);
        }
    }
}
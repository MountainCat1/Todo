using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teams.Service.Queries.GetTeamMembers;

namespace Teams.Api.Controllers;

[ApiController]
[Route("Team/{teamGuid}/Member/")]
public class MemberController : Controller
{
    private readonly IMediator _mediator;

    public MemberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetMembers([FromRoute] Guid teamGuid)
    {
        var query = new GetTeamMembersQuery(teamGuid);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
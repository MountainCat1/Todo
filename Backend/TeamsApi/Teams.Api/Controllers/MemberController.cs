using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teams.Domain.Abstractions;
using Teams.Domain.Entities;
using Teams.Domain.Repositories;
using Teams.Infrastructure.Abstractions;
using Teams.Infrastructure.Data;
using Teams.Infrastructure.Repositories;
using Teams.Service.Queries.GetTeamMembers;

namespace Teams.Api.Controllers;

[ApiController]
[Route("Team/{teamGuid}/Member/")]
public class MemberController : Controller
{
    private readonly IMediator _mediator;

    private readonly IUnitOfWork<TeamsDbContext> _unitOfWork;

    public MemberController(IMediator mediator, IUnitOfWork<TeamsDbContext> unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetMembers([FromRoute] Guid teamGuid)
    {
        var query = new GetTeamMembersQuery(teamGuid);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
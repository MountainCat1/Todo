using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teams.Infrastructure.Dto;
using Teams.Service;
using Teams.Service.Command.CreateTeam;
using Teams.Service.Command.CreateTodo;
using Teams.Service.Dto;
using Teams.Service.Queries.GetAllAccountTeams;
using Teams.Service.Queries.GetAllTeamTodos;
using Teams.Service.Queries.GetTeam;
using Teams.Service.Services;

namespace Teams.Api.Controllers;

[ApiController]
[Route("Team")]
public class TeamController : Controller
{
    private readonly IMediator _mediator;
    private readonly IAuthorizationService _authorizationService;
    private readonly IAccountService _accountService;
    public TeamController(IMediator mediator, IAuthorizationService authorizationService, IAccountService accountService)
    {
        _mediator = mediator;
        _authorizationService = authorizationService;
        _accountService = accountService;
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateTeamDto dto)
    {
        var accountGuidClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.PrimarySid);
        if (accountGuidClaim == null)
            return Unauthorized("Failed to authenticate account");

        var accountGuid = Guid.Parse(accountGuidClaim.Value);
        
        var command = new CreateTeamCommand(dto, accountGuid);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    [Authorize]
    [HttpGet("list")]
    public async Task<IActionResult> GetTeams()
    {
        var accountGuid = await _accountService.GetAccountGuidAsync(User);

        var query = new GetAllAccountTeamsQuery(accountGuid);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, query, Operations.UseRequest);
        if (!authorizationResult.Succeeded)
            return Forbid();

        var queryResult = await _mediator.Send(query);

        return Ok(queryResult);
    }
    
    [Authorize]
    [HttpGet("get/{teamGuid}")]
    public async Task<IActionResult> GetTeam(Guid teamGuid)
    {
        var query = new GetTeamQuery(teamGuid);

        var queryResult = await _mediator.Send(query);

        return Ok(queryResult);
    }
}
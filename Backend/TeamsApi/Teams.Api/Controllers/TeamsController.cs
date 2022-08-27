using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teams.Infrastructure.Dto;
using Teams.Service;
using Teams.Service.Command.CreateTeam;
using Teams.Service.Command.CreateTodo;
using Teams.Service.Dto;
using Teams.Service.Queries.GetAllTeamTodos;
using Teams.Service.Queries.GetTeam;

namespace Teams.Api.Controllers;

[ApiController]
[Route("Teams")]
public class TeamsController : Controller
{
    private readonly IMediator _mediator;
    private readonly IAuthorizationService _authorizationService;
    public TeamsController(IMediator mediator, IAuthorizationService authorizationService)
    {
        _mediator = mediator;
        _authorizationService = authorizationService;
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
    [HttpGet("{teamGuid}/todos")]
    public async Task<IActionResult> GetAllTodos([FromRoute] Guid teamGuid)
    {
        var query = new GetAllTeamTodosQuery(teamGuid);
        
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, query, Operations.UseRequest);
        if (!authorizationResult.Succeeded)
            return Forbid();
        
        var result = await _mediator.Send(query);
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("{teamGuid}/createTodo")]
    public async Task<IActionResult> CreateTodo([FromRoute] Guid teamGuid, [FromBody] CreateTodoDto createTodoDto)
    {
        var command = new CreateTodoCommand(teamGuid, createTodoDto);
        
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, command, Operations.UseRequest);
        if (!authorizationResult.Succeeded)
            return Forbid();
        
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }
}
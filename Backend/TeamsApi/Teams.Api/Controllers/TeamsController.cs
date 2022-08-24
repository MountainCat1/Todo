using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teams.Api.AuthorizationHandlers;
using Teams.Service.Command.CreateTeam;
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
        var getTeamQuery = new GetTeamQuery(teamGuid);
        var team = await _mediator.Send(getTeamQuery);
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, team, Operations.Read);
        if (!authorizationResult.Succeeded)
            return Forbid();
                
        var query = new GetAllTeamTodosQuery(teamGuid);
        var result = await _mediator.Send(query);
        
        return Ok(result);
    }
    
    
    /*[HttpGet]
    public async Task<IActionResult> Get([FromQuery] Guid? guid)
    {
        IBaseRequest query = guid != null 
            ? new GetTeamQuery((Guid)guid) 
            : new GetAllTeamsQuery();
        
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPut("{teamGuid}")]
    public async Task<IActionResult> Update([FromRoute] Guid teamGuid, [FromBody] UpdateTeamDto dto)
    {
        var command = new UpdateTeamCommand(teamGuid, dto);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{teamGuid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid teamGuid)
    {
        var command = new DeleteTeamCommand(teamGuid);
        await _mediator.Send(command);
        return Ok();
    }*/
}
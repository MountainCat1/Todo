using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teams.Service.Abstractions;
using Teams.Service.Command.CreateTeam;
using Teams.Service.Command.DeleteTeam;
using Teams.Service.Command.UpdateTeam;
using Teams.Service.Dto;
using Teams.Service.Queries.GetAllTeams;
using Teams.Service.Queries.GetTeam;

namespace Teams.Api.Controllers;

[ApiController]
[Route("Teams")]
public class TeamsController : Controller
{
    private readonly IMediator _mediator;

    public TeamsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Guid? guid)
    {
        IBaseRequest query = guid != null 
            ? new GetTeamQuery((Guid)guid) 
            : new GetAllTeamsQuery();
        
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeamDto dto)
    {
        var command = new CreateTeamCommand(dto);
        var result = await _mediator.Send(command);
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
    }
}
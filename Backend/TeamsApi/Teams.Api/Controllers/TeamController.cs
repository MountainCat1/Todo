using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teams.Service.Command.CreateTeamCommand;
using Teams.Service.Command.UpdateTeam;
using Teams.Service.Dto;
using Teams.Service.Queries.GetAllTeams;

namespace Teams.Api.Controllers;

[ApiController]
[Route("Team")]
public class TeamController : Controller
{
    private readonly IMediator _mediator;

    public TeamController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllTeamsQuery();
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
    public async Task<IActionResult> Update([FromQuery] Guid teamGuid, [FromBody] UpdateTeamDto dto)
    {
        var command = new UpdateTeamCommand(teamGuid, dto);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
        
    // TODO: do a controller for teams

}
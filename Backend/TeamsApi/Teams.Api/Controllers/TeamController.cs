using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teams.Service.Command.CreateTeamCommand;
using Teams.Service.Dto;

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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeamDto dto)
    {
        var command = new CreateTeamCommand(dto);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
        
    // TODO: do a controller for teams

}
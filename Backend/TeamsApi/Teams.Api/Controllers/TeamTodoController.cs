using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teams.Infrastructure.Dto;
using Teams.Service;
using Teams.Service.Command.CreateTodo;
using Teams.Service.Queries.GetAllAccountTeams;
using Teams.Service.Queries.GetAllTeamTodos;
using Teams.Service.Queries.GetTeam;
using Teams.Service.Services;

namespace Teams.Api.Controllers;

[ApiController]
[Route("Team/{teamGuid}")]
public class TeamTodoController : Controller
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IMediator _mediator;
    private readonly IAccountService _accountService;
    
    public TeamTodoController(IAuthorizationService authorizationService, IMediator mediator, IAccountService accountService)
    {
        _authorizationService = authorizationService;
        _mediator = mediator;
        _accountService = accountService;
    }

    [Authorize]
    [HttpGet("todos")]
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
    [HttpPost("createTodo")]
    public async Task<IActionResult> CreateTodo([FromRoute] Guid teamGuid, [FromBody] CreateTodoDto createTodoDto)
    {
        var command = new CreateTodoCommand(teamGuid, createTodoDto);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, command, Operations.UseRequest);
        if (!authorizationResult.Succeeded)
            return Forbid();

        await _mediator.Send(command);

        return Ok();
    }
}
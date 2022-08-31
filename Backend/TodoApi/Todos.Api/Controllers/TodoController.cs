using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todos.Service.Commands;
using Todos.Service.Commands.CreateTodo;
using Todos.Service.Commands.DeleteTodo;
using Todos.Service.Commands.UpdateTodo;
using Todos.Service.Dto;
using Todos.Service.Queries;
using Todos.Service.Queries.GetAllAccountTeamTodos;
using Todos.Service.Queries.GetAllTeamTodos;
using Todos.Service.Queries.GetAllTodos;
using Todos.Service.Queries.GetTodo;

namespace Todos.Api.Controllers;

[ApiController]
[Route("todo")]
public class TodoController : Controller
{
    private readonly IMediator _mediator;
    
    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get")]
    [Produces(typeof(ICollection<TodoDto>))]
    public async Task<IActionResult> GetTodos([FromQuery] Guid teamGuid)
    {
        var query = new GetAllTeamTodosQuery(teamGuid);
        
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("get/{teamGuid}")]
    [Produces(typeof(ICollection<TodoDto>))]
    public async Task<IActionResult> GetTodos([FromRoute] Guid teamGuid, [FromQuery] Guid accountGuid)
    {
        var query = new GetAllAccountTeamTodosQuery(teamGuid, accountGuid);
        
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todos.Service.Commands;
using Todos.Service.Commands.CreateTodo;
using Todos.Service.Commands.DeleteTodo;
using Todos.Service.Commands.UpdateTodo;
using Todos.Service.Dto;
using Todos.Service.Queries;
using Todos.Service.Queries.GetAllFilteredTodos;
using Todos.Service.Queries.GetAllTodos;
using Todos.Service.Queries.GetTodo;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController : Controller
{
    private readonly IMediator _mediator;
    
    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    [Produces(typeof(ICollection<TodoDto>))]
    public async Task<IActionResult> GetTodos([FromQuery] Guid? teamGuid, [FromQuery] Guid? userGuid)
    {
        var query = new GetAllFilteredTodosQuery(teamGuid, userGuid);
        
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("list")]
    [Produces(typeof(ICollection<TodoDto>))]
    public async Task<IActionResult> GetTodos()
    {
        var query = new GetAllTodosQuery();
        
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces(typeof(TodoDto))]
    public async Task<IActionResult> GetTodo([FromRoute] Guid guid)
    {
        var query = new GetTodoQuery(){Guid = guid};
        
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound();
        
        return Ok(result);
    }
    
    [HttpPost("")]
    public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDto createTodoDto)
    {
        var command = new CreateTodoCommand(createTodoDto);
        
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPut("{guid}")]
    public async Task<IActionResult> UpdateTodo([FromRoute] Guid guid, [FromBody] TodoDto todoDto)
    {
        var command = new UpdateTodoCommand(guid, todoDto);
        
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpDelete("{guid}")]
    public async Task<IActionResult> DeleteTodo([FromRoute] Guid guid)
    {
        var command = new DeleteTodoCommand(guid);
        
        await _mediator.Send(command);
        return Ok();
    }
}
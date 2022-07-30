using Microsoft.AspNetCore.Mvc;
using Todos.Service.Commands;
using Todos.Service.Dto;
using Todos.Service.Handlers;
using Todos.Service.Queries;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController : Controller
{
    private readonly ITodoQueryHandler _queryHandler;
    private readonly ITodoCommandHandler _commandHandler;

    public TodoController(ITodoQueryHandler queryHandler, ITodoCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet("")]
    [Produces(typeof(ICollection<TodoDto>))]
    public async Task<IActionResult> GetTodos([FromQuery] Guid? teamGuid, [FromQuery] Guid? userGuid)
    {
        var query = new GetFilteredTodosQuery(teamGuid, userGuid);
        
        var result = await _queryHandler.Handle(query, new CancellationToken());
        return Ok(result);
    }
    
    [HttpGet("list")]
    [Produces(typeof(ICollection<TodoDto>))]
    public async Task<IActionResult> GetTodos()
    {
        var query = new GetTodosQuery();
        
        var result = await _queryHandler.Handle(query, new CancellationToken());
        return Ok(result);
    }
    
    [HttpGet("{guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces(typeof(TodoDto))]
    public async Task<IActionResult> GetTodo([FromRoute] Guid guid)
    {
        var query = new GetTodoQuery(){Guid = guid};
        
        var result = await _queryHandler.Handle(query, new CancellationToken());

        if (result == null)
            return NotFound();
        
        return Ok(result);
    }
    
    [HttpPost("")]
    public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDto createTodoDto)
    {
        var command = new CreateTodoCommand()
        {
            CreateTodoDto = createTodoDto
        };
        
        await _commandHandler.Handle(command, new CancellationToken());
        return Ok();
    }
    
    [HttpPut("{guid}")]
    public async Task<IActionResult> UpdateTodo([FromRoute] Guid guid, [FromBody] TodoDto todoDto)
    {
        var command = new UpdateTodoCommand()
        {
            Guid = guid,
            TodoDto = todoDto
        };
        
        await _commandHandler.Handle(command, new CancellationToken());
        return Ok();
    }
    
    [HttpDelete("{guid}")]
    public async Task<IActionResult> DeleteTodo([FromRoute] Guid guid)
    {
        var command = new DeleteTodoCommand()
        {
            Guid = guid
        };
        
        await _commandHandler.Handle(command, new CancellationToken());
        return Ok();
    }
}